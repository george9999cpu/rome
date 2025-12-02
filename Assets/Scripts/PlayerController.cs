using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;      // max speed
    public float jumpForce = 7f;

    public float accel = 30f;         // how fast it speeds up
    public float decel = 25f;         // how fast it slows down

    public KeyCode L = KeyCode.A;
    public KeyCode R = KeyCode.D;
    public KeyCode jumpKey = KeyCode.Space;

    public Transform waterCheck;      // point under ship
    public float checkRadius = 0.1f;
    public LayerMask waterLayer;

    public bool onWater;              // see in inspector

    private Rigidbody2D rb;
    private float inputDir;           // -1 left, 1 right, 0 none

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   // get rb
    }

    void Update()
    {
        // read input here
        inputDir = 0f;
        if (Input.GetKey(L)) inputDir = -1f;
        else if (Input.GetKey(R)) inputDir = 1f;

        // check if touching water / ground
        if (waterCheck != null)
        {
            if (waterLayer.value == 0)
            {
                onWater = Physics2D.OverlapCircle(waterCheck.position, checkRadius);
            }
            else
            {
                onWater = Physics2D.OverlapCircle(waterCheck.position, checkRadius, waterLayer);
            }
        }
        else
        {
            onWater = false;
        }

        // jump only when on water
        if (Input.GetKeyDown(jumpKey) && onWater)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        float currentVx = rb.velocity.x;
        float targetVx = inputDir * moveSpeed;

        float rate = (Mathf.Abs(targetVx) > 0.01f) ? accel : decel;

        // use fixedDeltaTime for physics
        float newVx = Mathf.MoveTowards(currentVx, targetVx, rate * Time.fixedDeltaTime);

        rb.velocity = new Vector2(newVx, rb.velocity.y);

        // flip only based on input, not tiny noise
        if (inputDir != 0f)
        {
            Vector3 s = transform.localScale;
            s.x = (inputDir > 0f) ? Mathf.Abs(s.x) : -Mathf.Abs(s.x);
            transform.localScale = s;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (waterCheck != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(waterCheck.position, checkRadius);
        }
    }
}
