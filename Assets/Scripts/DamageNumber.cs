using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    void FixedUpdate() {
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y+0.1f);
    }
}
