using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculation : MonoBehaviour
{
    public static int damageCalculation(int damage, int defense) {
        return ((float)damage - (float)defense*0.5f) > 1 ? (int)((float)damage - (float)defense*0.5f) : 1;
    }
}
