using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int hp = 3;
    public GameObject damageTextPrefab;
    public Transform damageTextParent;

    public GameObject deathEffectPrefab;
    public float deathDestroyDelay = 0.5f;

    private bool dead = false;

    public void TakeDamage(int dmg)
    {
        if (dead) return;

        hp -= dmg;
        Debug.Log($"Enemy took {dmg} damage. HP now = {hp}");

        // damage text (as before)
        if (damageTextPrefab != null && damageTextParent != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            GameObject dt = Instantiate(damageTextPrefab, damageTextParent);
            dt.transform.position = screenPos;

            DamageText dtScript = dt.GetComponent<DamageText>();
            if (dtScript != null)
                dtScript.Setup(dmg);
        }

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        dead = true;
        Debug.Log("Enemy Die() called");

        if (deathEffectPrefab != null)
        {
            Debug.Log("Spawning death effect");
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("deathEffectPrefab is NOT assigned!");
        }

        // optional: hide sprite + collider
        var col = GetComponent<Collider2D>();
        if (col) col.enabled = false;

        var sr = GetComponent<SpriteRenderer>();
        if (sr) sr.enabled = false;

        Destroy(gameObject, deathDestroyDelay);
    }
}
