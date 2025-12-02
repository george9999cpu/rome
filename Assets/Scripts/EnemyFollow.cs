using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;   // drag your PLAYER/SHIP here in Inspector
    public float speed = 3f;

    void Update()
    {
        if (player == null) return;

        // Calculate direction
        Vector2 dir = (player.position - transform.position).normalized;

        // Move toward player
        transform.position += (Vector3)(dir * speed * Time.deltaTime);
    }
}
