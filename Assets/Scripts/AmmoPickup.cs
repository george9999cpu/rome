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

        switch (ammoType)
        {
            case AmmoType.Arrow:
                ps.arrowAmmo = Mathf.Min(ps.arrowAmmo + amount, ps.maxArrowAmmo);
                break;

            case AmmoType.Cannon:
                ps.cannonAmmo = Mathf.Min(ps.cannonAmmo + amount, ps.maxCannonAmmo);
                break;

            case AmmoType.Fire:
                ps.fireAmmo = Mathf.Min(ps.fireAmmo + amount, ps.maxFireAmmo);
                break;
        }


        if (ammoPopupPrefab != null)
{
    // find canvas
    Canvas canvas = FindObjectOfType<Canvas>();

    // spawn popup as child of Canvas
    GameObject popup = Instantiate(ammoPopupPrefab, canvas.transform);

    // set popup text
    string label = "+" + amount + " " + ammoType.ToString();
    popup.GetComponent<AmmoPopup>().SetText(label, new Color(1f, 0.85f, 0f)); 
    
    // position popup above the ship
    Vector3 screenPos = Camera.main.WorldToScreenPoint(other.transform.position);
    popup.transform.position = screenPos + new Vector3(0, 50, 0);
}

        Destroy(gameObject);
    }
}
