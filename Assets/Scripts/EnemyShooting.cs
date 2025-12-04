using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Fire point & rate")]
    public Transform firePoint;
    public float fireRate = 2f;


    [Header("Movement")]
public float moveSpeed = 2f;
private Transform playerTarget;


    [Header("Projectile setup")]
    public bool useMultipleTypes = false;     
    public GameObject singleProjPrefab;       
    public GameObject[] multiProjPrefabs;     

    [Header("Audio")]
    public AudioSource audioSrc;              
    public AudioClip arrowShotClip;
    public AudioClip cannonShotClip;
    public AudioClip fireShotClip;

    [Header("Detection")]
    public string playerTag = "Ship";          // player tag
    private bool playerInRange = false;        // only shoot when TRUE

    private float nextShotTime;


    // ---------------------------
    // UPDATE: Shoot only in range
    // ---------------------------
    void Update()
    {

        if (playerInRange && playerTarget != null)
    {
        // ⭐ Move toward the player
        Vector3 dir = (playerTarget.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
        if (!playerInRange) return;

        if (Time.time >= nextShotTime)
        {
            nextShotTime = Time.time + fireRate;
            Shoot();
        }
    }


    // ---------------------------
    // SHOOTING LOGIC
    // ---------------------------
    void Shoot()
    {
        GameObject prefabToUse = null;

        if (useMultipleTypes)
        {
            if (multiProjPrefabs == null || multiProjPrefabs.Length == 0) return;
            prefabToUse = multiProjPrefabs[Random.Range(0, multiProjPrefabs.Length)];
        }
        else
        {
            if (singleProjPrefab == null) return;
            prefabToUse = singleProjPrefab;
        }

        GameObject proj = Instantiate(prefabToUse, firePoint.position, firePoint.rotation);

        // ⭐ Always shoot LEFT
        ProjectileMove pm = proj.GetComponent<ProjectileMove>();
        if (pm != null)
            pm.dir = Vector2.left;

        // Mark projectile as enemy
        ProjectileDamage pd = proj.GetComponent<ProjectileDamage>();
        if (pd != null)
            pd.fromPlayer = false;

        // Play correct sound
        PlayShotSFX(prefabToUse);
    }


    // ---------------------------
    // PLAY SFX based on name
    // ---------------------------
    void PlayShotSFX(GameObject projPrefab)
    {
        if (audioSrc == null) return;

        string n = projPrefab.name.ToLower();
        AudioClip clip = null;

        if (n.Contains("arrow"))
            clip = arrowShotClip;
        else if (n.Contains("cannon"))
            clip = cannonShotClip;
        else if (n.Contains("fire"))
            clip = fireShotClip;

        if (clip != null)
            audioSrc.PlayOneShot(clip);
    }


    // ---------------------------
    // DETECTION TRIGGER
    // ---------------------------
    void OnTriggerEnter2D(Collider2D other)
    {


         if (other.CompareTag(playerTag))
    {
        playerInRange = true;
        playerTarget = other.transform;   // ⭐ store reference to player
    }
    }

    
}
