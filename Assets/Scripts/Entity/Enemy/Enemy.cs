
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField]
    private int health;
    public int defense;

    int startingDefense;
    [SerializeField]
    private Transform damageIndicator;
    [SerializeField]
    private float corpseLifetime;

    public bool dead;

    public Animator animator;

    bool direction;
    public bool flipping;
    public Transform sprite;

    public List<EffectInstance> statuses = new List<EffectInstance>();

    Dictionary<string, int> statEffects = new Dictionary<string, int>();

    [SerializeField] EnemyMovement movement;
    [SerializeField] EnemyAttack[] attacks;
    [SerializeField] int[] weights;
    [SerializeField] float[] timestamps;
    [SerializeField] EnemyDeath enemyDeath;

    public bool attacking = false;
    float atkTimestamp;
    int curAtkIndex;

    public void Awake()
    {
        startingDefense = defense;
        timestamps = new float[attacks.Length];
    }

    public int getBaseDefense() {
        return startingDefense;
    }

    // Applies Damagess
    public void takeDamage(int damage) {
        if(this.dead) return;

        int finalDamage = (int)((float)DamageCalculation.damageCalculation(damage, defense)*(1+((float)getStatEffect("damagetakenpercent"))/100));

        health-=finalDamage;

        onDamage(damage);

        UIManager.Instance.createFloatingText(damageIndicator.position, finalDamage.ToString(), 0.2f);

        if(health<=0) {
            Destroy(this.gameObject, corpseLifetime);
            animator.SetBool("Dead", true);
            dead = true;

            if(enemyDeath!=null) {
                enemyDeath.onDeath();
            }

            this.enabled = false;
            
        }
    }

    public void unconditionalHealthChange(int change) {
        health+=change;
    }

    // Getter Method For Health
    public int getHealth() {
        return health;
    }

    public bool getDetected() {
        return movement.detected;
    }

    public virtual void onDamage(int damage) {

    }

    private void flipCheck() {
        if(Player.Instance.transform.position.x>this.gameObject.transform.position.x&&!direction) {
            flip();
        } else if(Player.Instance.transform.position.x<this.gameObject.transform.position.x&&direction) {
            flip();
        }  
    }

    private void flip() {
        direction=!direction;
        sprite.Rotate(0f, 180f, 0f);
    }

    void Update()
    {
        updateEnemy();
        updateStats();

        if(flipping)    flipCheck();

        

        foreach(EffectInstance s in statuses) {
            if(s!=null) {
                s.updateEffect();
            }
        }
        //Debug.Log(statuses.Exists(effectOver));
        statuses.RemoveAll(isOver);
    }

    void FixedUpdate()
    {
        movement.updateMovement();

        if(!attacking) {
            attackCycle();
        } else {
            attacks[curAtkIndex].updateAttack(this, atkTimestamp);
        }
    }

    public void setMovementActive(bool x) {
        movement.disabled = !x;
    }
    
    public void finishAttack() {
        attacking = false;
        timestamps[curAtkIndex] = Time.time;
        setMovementActive(true);
    }

    void attackCycle() {

        List<int> atksIndex = new List<int>();

        for(int i = 0; i < attacks.Length; i++) {
            attacks[i].canAttack(this);
            if(timestamps[i] + attacks[i].attackCooldown < Time.time && attacks[i].canAttack(this)) {
                atksIndex.Add(i);
            }
        }

        int index = Util.weightedRoll(atksIndex, weights);

        if(index != -1) {
            curAtkIndex = index;

            attacking = true;
            attacks[curAtkIndex].beginAttack(this);
            atkTimestamp = Time.time;
        }
    }

    private bool isOver(EffectInstance s) {
        //Debug.Log(s.effectOver);

        if(s.effectOver) {
            return true;
        }

        return false;
    }

    public virtual void updateEnemy() {

    }

    public void addStatus(EffectInstance s) {
        foreach(EffectInstance e in statuses) {
            if(e.statusEffect.id.Equals(s.statusEffect.id)) {
                e.length = s.length;
                return;
            }
        }
        statuses.Add(s);

        statEffects = updateStatEffects();
        
        
    }

    Dictionary<string, int> updateStatEffects() {
        Dictionary<string, int> result = new Dictionary<string, int>();

        foreach(EffectInstance s in statuses) {
            Dictionary<string, int> x = s.statusEffect.statEffects();
            foreach(var(Key, Value) in x) {
                try
                {
                    result.Add(Key, Value);
                }
                catch (ArgumentException)
                {
                    result[Key] += Value;
                }
            }
        }

        return result;
    }

    public virtual void updateStats() {
            this.defense = (int)(getBaseDefense()*(1+((float)getStatEffect("defensepercent"))/100));
    }

    public int getStatEffect(string stat) {
        int value = 0;
        statEffects.TryGetValue(stat, out value);
        return value;
    }
}
