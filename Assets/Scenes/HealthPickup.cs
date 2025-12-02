using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 20;

    [Header("Floating Settings")]
    public float floatSpeed = 1f;
    public float floatHeight = 0.1f;

    [Header("Popup")]
    public GameObject healthPopupPrefab;  // drag popup prefab here

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // bob on the water
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ship"))
            return;

        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph == null)
            ph = other.GetComponentInChildren<PlayerHealth>();

        if (ph == null)
            return;

        // heal the player
        ph.AddHealth(healAmount);

        // show green +HP popup
        SpawnHealthPopup(other.transform.position);

        Destroy(gameObject);
    }

    void SpawnHealthPopup(Vector3 worldPos)
    {
        if (healthPopupPrefab == null)
        {
            Debug.LogWarning("HealthPickup: healthPopupPrefab is NULL, no popup spawned.");
            return;
        }

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogWarning("HealthPickup: No Canvas found in scene.");
            return;
        }

        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogWarning("HealthPickup: No Main Camera found (tag your camera as MainCamera).");
            return;
        }

        GameObject popup = Instantiate(healthPopupPrefab, canvas.transform);

        Vector3 screenPos = cam.WorldToScreenPoint(worldPos);
        popup.transform.position = screenPos + new Vector3(0f, 50f, 0f);

        AmmoPopup ap = popup.GetComponent<AmmoPopup>();
        if (ap == null)
        {
            Debug.LogWarning("HealthPickup: Popup prefab has no AmmoPopup component!");
            return;
        }

        // 💚 HEALTH COLOR HERE
        ap.SetText("+" + healAmount + " HP", new Color(0f, 0.6f, 0f)); // green-ish
    }
}
