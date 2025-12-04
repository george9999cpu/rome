using UnityEngine;

public class PlayerShooting : MonoBehaviour
{


    [Header("Shoot Sounds")]
public AudioClip arrowSfx;
public AudioClip cannonSfx;
public AudioClip fireSfx;

[Header("Pickup Sounds")]
public AudioClip arrowPickupSfx;
public AudioClip cannonPickupSfx;
public AudioClip firePickupSfx;



    public Transform firePoint;

    public GameObject arrowPrefab;
    public GameObject cannonballPrefab;
    public GameObject fireshotPrefab;

    public KeyCode arrowKey = KeyCode.E;
    public KeyCode cannonKey = KeyCode.R;
    public KeyCode fireKey = KeyCode.T;

    // shot speeds
    public float arrowSpeed = 12f;
    public float cannonSpeed = 8f;
    public float fireSpeed = 6f;

    // dmg values from doc
    public int arrowDamage = 10;
    public int cannonDamage = 40;
    public int fireDamage = 70;

    // reload times from doc
    public float arrowReload = 2f;
    public float cannonReload = 6f;
    public float fireReload = 9f;

    // timers
    public float arrowTimer;
    public float cannonTimer;
    public float fireTimer;

    // ===== AMMO PER TYPE =====
    [Header("Arrow Ammo")]
    public int arrowAmmo = 20;
    public int maxArrowAmmo = 60;

    [Header("Cannon Ammo")]
    public int cannonAmmo = 5;
    public int maxCannonAmmo = 20;

    [Header("Fire Ammo")]
    public int fireAmmo = 3;
    public int maxFireAmmo = 10;

   
    public AudioSource src;

    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Y))
    {
        Debug.Log("TEST: Adding arrow ammo from keyboard");
        AddArrowAmmo(5);
    }
        // tick timers down
        if (arrowTimer > 0f) arrowTimer -= Time.deltaTime;
        if (cannonTimer > 0f) cannonTimer -= Time.deltaTime;
        if (fireTimer > 0f) fireTimer -= Time.deltaTime;

        // ===== ARROW SHOT =====
        if (Input.GetKeyDown(arrowKey) && arrowTimer <= 0f)
        {
            if (arrowAmmo > 0)
            {
                ShootProjectile(arrowPrefab, arrowSpeed, arrowDamage);
                arrowTimer = arrowReload;
                arrowAmmo--;

                // play sound
                if (src != null && arrowSfx != null)
                    src.PlayOneShot(arrowSfx);
            }
            else
            {
                Debug.Log("No arrow ammo!");
            }
        }

        // ===== CANNON SHOT =====
        if (Input.GetKeyDown(cannonKey) && cannonTimer <= 0f)
        {
            if (cannonAmmo > 0)
            {
                ShootProjectile(cannonballPrefab, cannonSpeed, cannonDamage);
                cannonTimer = cannonReload;
                cannonAmmo--;

                if (src != null && cannonSfx != null)
                    src.PlayOneShot(cannonSfx);
            }
            else
            {
                Debug.Log("No cannon ammo!");
            }
        }

        // ===== FIRE SHOT =====
        if (Input.GetKeyDown(fireKey) && fireTimer <= 0f)
        {
            if (fireAmmo > 0)
            {
                ShootProjectile(fireshotPrefab, fireSpeed, fireDamage);
                fireTimer = fireReload;
                fireAmmo--;

                if (src != null && fireSfx != null)
                    src.PlayOneShot(fireSfx);
            }
            else
            {
                Debug.Log("No fire ammo!");
            }
        }
    }

    void ShootProjectile(GameObject prefab, float speed, int dmg)
    {
        if (prefab == null || firePoint == null) return;

        // spawn
        GameObject b = Instantiate(prefab, firePoint.position, Quaternion.identity);

        // move
        ProjectileMove pm = b.GetComponent<ProjectileMove>();
        if (pm != null)
        {
            // decide left / right based on ship facing
            Vector2 dir = (transform.localScale.x > 0) ? Vector2.right : Vector2.left;
            pm.dir = dir;
            pm.speed = speed;
        }

        // set damage, mark as player shot
        ProjectileDamage pd = b.GetComponent<ProjectileDamage>();
        if (pd != null)
        {
            pd.damage = dmg;
            pd.fromPlayer = true;
        }
    }

  public void AddArrowAmmo(int amount)
{
    if (amount <= 0) return;

    int old = arrowAmmo;

    arrowAmmo = Mathf.Clamp(arrowAmmo + amount, 0, maxArrowAmmo);

    if (arrowAmmo > old)
    {
        Debug.Log($"Arrow ammo pickup: +{amount}, now {arrowAmmo}");

        if (src != null && arrowPickupSfx != null)
            src.PlayOneShot(arrowPickupSfx);
    }
}

public void AddCannonAmmo(int amount)
{
    if (amount <= 0) return;

    int old = cannonAmmo;

    cannonAmmo = Mathf.Clamp(cannonAmmo + amount, 0, maxCannonAmmo);

    if (cannonAmmo > old)
    {
        Debug.Log($"Cannon ammo pickup: +{amount}, now {cannonAmmo}");

        if (src != null && cannonPickupSfx != null)
            src.PlayOneShot(cannonPickupSfx);
    }
}

public void AddFireAmmo(int amount)
{
    if (amount <= 0) return;

    int old = fireAmmo;

    fireAmmo = Mathf.Clamp(fireAmmo + amount, 0, maxFireAmmo);

    if (fireAmmo > old)
    {
        Debug.Log($"Fire ammo pickup: +{amount}, now {fireAmmo}");

        if (src != null && firePickupSfx != null)
            src.PlayOneShot(firePickupSfx);
    }
}

public void ResetAmmo()
{
    arrowAmmo = 20;
    cannonAmmo = 10;
    fireAmmo = 3;

    // If you have ammo UI events, trigger them here:
    // OnAmmoChanged?.Invoke(arrowAmmo, cannonAmmo, fireAmmo);
}


    public bool ArrowReady()
    {
        return arrowTimer <= 0f && arrowAmmo > 0;
    }

    public bool CannonReady()
    {
        return cannonTimer <= 0f && cannonAmmo > 0;
    }

    public bool FireReady()
    {
        return fireTimer <= 0f && fireAmmo > 0;
    }
}
