using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileType type;
    public float speed = 10f;
    public float lifeTime = 3f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // simple type setup
        if (type == ProjectileType.Arrow)
        {
            rb.gravityScale = 0f;
        }
        else if (type == ProjectileType.Cannonball)
        {
            rb.gravityScale = 1f;
        }
        else if (type == ProjectileType.FireShot)
        {
            rb.gravityScale = 0.2f;
        }

        Destroy(gameObject, lifeTime);
    }

    public void Launch(Vector2 dir)
    {
        rb.velocity = dir * speed;
    }
}
