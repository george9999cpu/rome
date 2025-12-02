using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damage = 1;
    public bool fromPlayer = true;   // true = hurts enemies, false = hurts player

    void OnTriggerEnter2D(Collider2D other)
    {
        if (fromPlayer)
        {
            // player shots damage enemies
            EnemyHealth eh = other.GetComponent<EnemyHealth>();
            if (eh != null)
            {
                eh.TakeDamage(damage);
                Destroy(gameObject);
            }}
        
        else
        {
            // enemy shots damage the player
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

}