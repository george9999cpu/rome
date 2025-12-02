using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed = 10f;       // how fast it goes
    public Vector2 dir = Vector2.right;  // set from shooter
    public float lifeTime = 3f;     // destroy after some time

    void Start()
    {
        Destroy(gameObject, lifeTime);   // auto delete later
    }

    void Update()
    {
        // move every frame
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
