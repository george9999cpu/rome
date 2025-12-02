using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Fire : MonoBehaviour
{
    public ProjectileType type;
    public float speed = 10f;
    public float lifeTime = 3f;

    private Rigidbody2D rb;
    private float defaultGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // remember original gravity
        defaultGravity = rb.gravityScale;

        // handle type based movement
        if (type == ProjectileType.Arrow)
        {
            rb.gravityScale = 0f;        // straight
        }
        else if (type == ProjectileType.Cannonball)
        {
            rb.gravityScale = defaultGravity; // arc
        }
        else if (type == ProjectileType.FireShot)
        {
            rb.gravityScale = 0.2f;      // slight drop
        }

        // destroy later
        Destroy(gameObject, lifeTime);
    }

    public void Launch(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }
}

