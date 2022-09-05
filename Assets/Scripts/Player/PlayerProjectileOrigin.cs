using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileOrigin : MonoBehaviour
{
    // Makes Projectile Origin Point In The Correct Direction
    void Update()
    {
        Vector2 position = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - position;
        transform.right = direction;
    }

    // Fires A Projectile Based On The Input
    public void fire(GameObject projectile, float power) {
        GameObject newProj = Instantiate(projectile, transform.position, transform.rotation);
        newProj.GetComponent<Rigidbody2D>().velocity = transform.right*power;
    }
}
