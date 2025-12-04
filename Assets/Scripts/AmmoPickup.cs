using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public enum AmmoType { Arrow, Cannon, Fire }

    public GameObject ammoPopupPrefab; 
    public AmmoType ammoType = AmmoType.Arrow;
    public int amount = 5;

    [Header("Floating Settings")]
    public float floatSpeed = 1f;      // how fast it bobs
    public float floatHeight = 0.3f;   // how high it moves up/down

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // simple bobbing on water
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // make sure it's the player ship
        if (!other.CompareTag("Ship"))
            return;

        PlayerShooting ps = other.GetComponent<PlayerShooting>();
        if (ps == null)
            ps = other.GetComponentInChildren<PlayerShooting>();

        if (ps == null)
            return;

        // ✅ use the functions that clamp + play SFX
        switch (ammoType)
        {
            case AmmoType.Arrow:
                ps.AddArrowAmmo(amount);
                break;

            case AmmoType.Cannon:
                ps.AddCannonAmmo(amount);
                break;

            case AmmoType.Fire:
                ps.AddFireAmmo(amount);
                break;
        }

        // popup UI
        if (ammoPopupPrefab != null)
        {
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas != null)
            {
                GameObject popup = Instantiate(ammoPopupPrefab, canvas.transform);

                string label = "+" + amount + " " + ammoType.ToString();
                popup.GetComponent<AmmoPopup>()
                     .SetText(label, new Color(1f, 0.85f, 0f)); 

                Vector3 screenPos = Camera.main.WorldToScreenPoint(other.transform.position);
                popup.transform.position = screenPos + new Vector3(0, 50, 0);
            }
        }

        Destroy(gameObject);
    }
}
