using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
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
     public  float arrowTimer;
     public  float cannonTimer;
     public  float fireTimer;

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

    void Update()
    {
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

    // ===== CALLED BY PICKUPS =====
    public void AddArrowAmmo(int amount)
    {
        arrowAmmo += amount;
        if (arrowAmmo > maxArrowAmmo) arrowAmmo = maxArrowAmmo;
        if (arrowAmmo < 0) arrowAmmo = 0;
    }

    public void AddCannonAmmo(int amount)
    {
        cannonAmmo += amount;
        if (cannonAmmo > maxCannonAmmo) cannonAmmo = maxCannonAmmo;
        if (cannonAmmo < 0) cannonAmmo = 0;
    }

    public void AddFireAmmo(int amount)
    {
        fireAmmo += amount;
        if (fireAmmo > maxFireAmmo) fireAmmo = maxFireAmmo;
        if (fireAmmo < 0) fireAmmo = 0;
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
