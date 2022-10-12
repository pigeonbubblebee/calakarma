using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats Settings")]
    public int health;
    public int mana;
    public int defense;

    public int startingMaxHealth = 100;

    public int maxHealth;
    private float healthTimestamp = 0.0f;
    public float healthRegenerationRate = 0.1f;

    [Header("Magic Settings")]

    public int maxMana;
    public float maxOverload;
    private float manaTimestamp = 0.0f;
    public float manaRegenerationRate = 0.1f;

    private int overload;
    public bool overloaded;

    [Header("Stealth Settings")]

    public float maxStealth;
    [SerializeField]
    private int stealth;
    public bool stealthed;
    private float stealthTimestamp = 0.0f;
    public float stealthRegenerationRate = 0.1f;

    [Header("Charge Settings")]
    
    public float maxCharge;
    [SerializeField]
    private int charge;
    public bool charged;
    private float chargeTimestamp = 0.0f;
    public float chargeRate = 0.1f;

    public bool charging;

    [Header("Dash Settings")]
    
    public float maxDash;
    [SerializeField]
    private int dash;
    public bool dashReady;
    public float dashSpeed;
    private float dashTimestamp = 0.0f;
    public float dashRate = 0.1f;

    [Header("Player Stats References")]

    public HorizontalBar manaBar;
    public HorizontalBar healthBar;
    public SpecialBar specialBar;

    public Dictionary<string, int> mementoStatBonuses = new Dictionary<string, int>();
    public Dictionary<string, int> jewelryStatBonuses = new Dictionary<string, int>();
    public Dictionary<string, int> effectsStatBonuses = new Dictionary<string, int>();

    Dictionary<string, int> statBonuses = new Dictionary<string, int>();

    public List<EffectInstance> statuses = new List<EffectInstance>();

    // Applies Damage To Player
    public void takeDamage(int damage) {
        health=Mathf.Max(health-DamageCalculation.damageCalculation(damage, defense), 0);
        if(health<=0) {
            health = maxHealth;
            NodeManager.currentEntranceName = "respawn";
            SceneManager.LoadScene(Player.respawnScene);
            Save.AutoSave();
        }
        resetStealth();
    }

    public void setMementoStatBonuses(Dictionary<string, int> x) {
        mementoStatBonuses = x;

        statBonuses = mementoStatBonuses;

        foreach(string key in jewelryStatBonuses.Keys) {
            addToStat(key, x[key], statBonuses);
        }

        foreach(string key in effectsStatBonuses.Keys) {
            addToStat(key, effectsStatBonuses[key], statBonuses);
        }
    }

    public void setJewelryStatBonuses(Dictionary<string, int> x) {
        jewelryStatBonuses = x;

        statBonuses = mementoStatBonuses;

        foreach(string key in jewelryStatBonuses.Keys) {
            addToStat(key, x[key], statBonuses);
        }

        foreach(string key in effectsStatBonuses.Keys) {
            addToStat(key, effectsStatBonuses[key], statBonuses);
        }
    }

    public void setEffectsStatBonuses(Dictionary<string, int> x) {
        effectsStatBonuses = x;

        statBonuses = mementoStatBonuses;

        foreach(string key in jewelryStatBonuses.Keys) {
            addToStat(key, x[key], statBonuses);
        }

        foreach(string key in effectsStatBonuses.Keys) {
            addToStat(key, effectsStatBonuses[key], statBonuses);
        }
    }

    void addToStat(string name, int amount, Dictionary<string, int> statBonuses) {
        try
        {
            statBonuses.Add(name, amount);
        }
        catch (ArgumentException)
        {
            statBonuses[name] = statBonuses[name]+amount;
        }
    }

    // Updates Stats Based On Regeneration Rates And Updates UI
    void Update()
    {
        updateMana();
        updateStealth();
        updateCharge();
        updateHealth();
        updateDash();

        updateBars();

        updateStatBonuses();

        updateEffects();
    }

    private void updateEffects() {
        foreach(EffectInstance s in statuses) {
            if(s!=null) {
                s.updateEffectPlayer();
            }
        }
        //Debug.Log(statuses.Exists(effectOver));
        statuses.RemoveAll(isOver);
    }

    private bool isOver(EffectInstance s) {
        //Debug.Log(s.effectOver);

        if(s.effectOver) {
            return true;
        }

        return false;
    }

    // Update HUD Bars
    private void updateBars() {
        manaBar.changeAmount(mana/(float)maxMana);
        healthBar.changeAmount(health/(float)maxHealth);

        WeaponData.WeaponDamageType weaponDamageType = WeaponData.WeaponDamageType.Classless;
        if(Player.Instance.playerCombat.getEquipped()!=null) {
            weaponDamageType = ((WeaponData)(Player.Instance.playerCombat.getEquipped().getItemData())).weaponDamageType;
            specialBar.setWeaponDamageType(weaponDamageType);
        } else {
            specialBar.setWeaponDamageType(WeaponData.WeaponDamageType.Classless);
        }
        
        if(weaponDamageType == WeaponData.WeaponDamageType.Melee)    specialBar.changeAmount(charge/(float)maxCharge);
        if(weaponDamageType == WeaponData.WeaponDamageType.Ranged)    specialBar.changeAmount(stealth/(float)maxStealth);
        if(weaponDamageType == WeaponData.WeaponDamageType.Magic)    specialBar.changeAmount(overload/(float)maxOverload);
    }

    // Updates Mana And Overload System
    private void updateMana() {
        if(Time.time > manaTimestamp)  {
            manaTimestamp = Time.time+manaRegenerationRate;
            if(mana<maxMana)    mana++;
            else if(overload<maxOverload)   overload++;
        }

        if(overload >= maxOverload) {
            overloaded = true;
        } else {
            overloaded = false;
        }
    }

    // Updates Health System
    private void updateHealth() {
        if(Time.time > healthTimestamp)  {
            healthTimestamp = Time.time+healthRegenerationRate;
            if(health<maxHealth)    health++;
        }
    }

    // Updates Stealth System
    private void updateStealth() {
        if(Time.time > stealthTimestamp)  {
            stealthTimestamp = Time.time+stealthRegenerationRate;
            if(stealth<maxStealth)    stealth++;
        }

        if(stealth >= maxStealth) {
            stealthed = true;
        } else {
            stealthed = false;
        }
    }

    // Updates Dash System
    private void updateDash() {
        if(Time.time > dashTimestamp)  {
            dashTimestamp = Time.time+dashRate;
            if(dash<maxDash)    dash++;
        }

        if(dash >= maxDash) {
            dashReady = true;
        } else {
            dashReady = false;
        }
    }

    // Updates Charge System
    private void updateCharge() {
        Item equipped = (Player.Instance.playerCombat.getEquipped());
        if(equipped!=null) {
            if(((WeaponData)(equipped.getItemData())).weaponType == WeaponData.WeaponType.Sword) {
                SwordData sword = ((SwordData)((WeaponData)(Player.Instance.playerCombat.getEquipped().getItemData())));
                maxCharge = (sword.charge100PercentSpeed+sword.chargeSpeed)*10;
            }
        }

        if(Time.time > chargeTimestamp)  {
            chargeTimestamp = Time.time+chargeRate;
            if(charge<maxCharge && charging)    charge++;
        }

        if(charge >= maxCharge) {
            charged = true;
        } else {
            charged = false;
        }
    }

    // Resets Overload System. Called After Overloaded Shot
    public void resetOverload() {
        overload = 0;
        overloaded = false;
    }

    // Resets Stealth System
    public void resetStealth() {
        stealth = 0;
        stealthed = false;
    }

    // Resets Charge System
    public void resetCharge() {
        charge = 0;
        charged = false;
    }

    // Resets Dash System
    public void resetDash() {
        dash = 0;
        dashReady = false;
    }

    private void updateStatBonuses() {
        maxHealth = (int)(startingMaxHealth*(1+((float)getStatBonus("hppercent")/100f)));
        defense = (int)(getStatBonus("defense")*(1+((float)getStatBonus("defensepercent")/100f)));
    }

    // Updates Mana
    public void subtractMana(int mana) {
        this.mana = Mathf.Max(this.mana-mana, 0);
    }

    public int getStatBonus(string stat) {
        int value = 0;
        statBonuses.TryGetValue(stat, out value);
        return value;
    }

    public Dictionary<string, int> getStatBonuses() {
        return statBonuses;
    }

    public void addStatus(EffectInstance s) {
        foreach(EffectInstance e in statuses) {
            if(e.statusEffect.id.Equals(s.statusEffect.id)) {
                e.length = s.length;
                return;
            }
        }
        statuses.Add(s);

        effectsStatBonuses = updateStatusEffects();

        setEffectsStatBonuses(effectsStatBonuses);
    }

    Dictionary<string, int> updateStatusEffects() {
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
}