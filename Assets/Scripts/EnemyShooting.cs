using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Fire point & rate")]
    public Transform firePoint;
    public float fireRate = 2f;

    [Header("Projectile setup")]
    public bool useMultipleTypes = false;     // OFF = single prefab, ON = multiple
    public GameObject singleProjPrefab;       // for normal enemies
    public GameObject[] multiProjPrefabs;     // for special enemies

    private float nextShotTime;

    void Update()
    {
        if (Time.time >= nextShotTime)
        {
            nextShotTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
{
    GameObject prefabToUse = null;

    if (useMultipleTypes)
    {
        if (multiProjPrefabs == null || multiProjPrefabs.Length == 0) return;
        int i = Random.Range(0, multiProjPrefabs.Length);
        prefabToUse = multiProjPrefabs[i];
    }
    else
    {
        if (singleProjPrefab == null) return;
        prefabToUse = singleProjPrefab;
    }

    GameObject proj = Instantiate(prefabToUse, firePoint.position, firePoint.rotation);

    // ⭐ Force projectile direction to LEFT always
    ProjectileMove pm = proj.GetComponent<ProjectileMove>();
    if (pm != null)
    {
        pm.dir = Vector2.left;
    }

    // still mark it as enemy projectile
    ProjectileDamage pd = proj.GetComponent<ProjectileDamage>();
    if (pd != null)
    {
        pd.fromPlayer = false;
    }
}
}
