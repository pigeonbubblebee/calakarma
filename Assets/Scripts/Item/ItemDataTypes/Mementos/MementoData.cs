using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "MementoData", menuName = "Calakarma/MementoItem", order = 5)]
public class MementoData : ItemData
{
    public EmotionBasic emotionBasic;
    public EmotionMoving emotionMoving;
    public EmotionPrimal emotionPrimal;
    public EmotionFinal emotionFinal;

    public string[] otherAttributes;
    public string[] otherAttributesBasic;
    public string[] otherAttributesMoving;
    public string[] otherAttributesPrimal;
    public string[] otherAttributesFinal;

    // Enums Used To Determine Emotions
    public enum EmotionBasic {
        Sadness,
        Happiness,
        Anger
    }

    public enum EmotionMoving {
        Regret,
        Sorrow,
        Grief,
        Gratitude,
        Celebration,
        Excitement,
        Lament,
        Revolt,
        Belligerence
    }

    public enum EmotionPrimal {
        Denial,
        Isolation,
        Anxiety,
        Depression,
        Cynicality,
        Hospitality,
        Lightheartedness,
        Festivity,
        Zeal,
        Inspiration,
        Hatred,
        Purpose,
        Rebellion,
        Implacability,
        Argument
    }

    public enum EmotionFinal {
        Sage,
        Tranquil,
        Control
    }

    public virtual void onProjectileCollision(Collision2D collision, int emotionLevel) {
        
    }

    public virtual void onMagicProjectileCollision(Collision2D collision, int emotionLevel) {
        
    }

    public virtual void onMeleeCollision(Collider2D collision, int emotionLevel) {
        
    }

    public virtual Dictionary<string, int> updateStatBonuses(Player player, int emotionLevel) {
        resetStats();
        Dictionary<string, int> statBonuses = new Dictionary<string, int>();

        if(emotionLevel > 0) {
            switch(emotionBasic) {
                case EmotionBasic.Sadness:
                    addToStat("defensepercent", 10, statBonuses);
                    break;
                case EmotionBasic.Happiness:
                    addToStat("attackspeedpercent", 10, statBonuses);
                    break;
                case EmotionBasic.Anger:
                    addToStat("damagepercent", 10, statBonuses);
                    break;
            }
        }

        if(emotionLevel > 1) {
            switch(emotionMoving) {
                case EmotionMoving.Regret:
                    addToStat("defensepercent", 15, statBonuses);
                    break;
                case EmotionMoving.Sorrow:
                    addToStat("hppercent", 10, statBonuses);
                    break;
                case EmotionMoving.Grief:
                    addToStat("defensepercent", 5, statBonuses);
                    addToStat("hppercent", 5, statBonuses);
                    break;
            }
        }

        if(emotionLevel > 2) {
            switch(emotionPrimal) {
                case EmotionPrimal.Denial:
                    addToStat("defensepercent", 5, statBonuses);
                    addToStat("hppercent", 15, statBonuses);
                    break;
                case EmotionPrimal.Isolation:
                    addToStat("defensepercent", 10, statBonuses);
                    addToStat("hppercent", 10, statBonuses);
                    break;
                case EmotionPrimal.Anxiety:
                    addToStat("defensepercent", 20, statBonuses);
                    break;
                case EmotionPrimal.Depression:
                    addToStat("hppercent", 20, statBonuses);
                    break;
                case EmotionPrimal.Cynicality:
                    addToStat("defensepercent", 15, statBonuses);
                    addToStat("hppercent", 5, statBonuses);
                    break;
            }
        }

        if(emotionLevel > 3) {
            switch(emotionFinal) {
                case EmotionFinal.Sage:
                    addToStat("defensepercent", 40, statBonuses);
                    addToStat("hppercent", 30, statBonuses);
                    break;
            }
        }

        return statBonuses;
    }

    public void addToStat(string name, int amount, Dictionary<string, int> statBonuses) {
        try
        {
            statBonuses.Add(name, amount);
        }
        catch (ArgumentException)
        {
            statBonuses[name] = statBonuses[name]+amount;
        }
    }

    public void setStat(string name, int amount, Dictionary<string, int> statBonuses) {
        try
        {
            statBonuses.Add(name, amount);
        }
        catch (ArgumentException)
        {
            statBonuses[name] = amount;
        }
    }

    private void resetStats() {
        Dictionary<string, int> statBonuses = new Dictionary<string, int>();
    }

    public virtual string[] getOtherAttributes(int emotionLevel) {
        switch(emotionLevel) {
            case 0:
                return otherAttributes;
            case 1:
                return otherAttributesBasic;
            case 2:
                return otherAttributesMoving;
            case 3:
                return otherAttributesPrimal;
            case 4:
                return otherAttributesFinal;
        }

        return new string[0];
    }
}
