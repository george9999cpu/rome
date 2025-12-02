using UnityEngine;
using TMPro;

public class AmmoPopup : MonoBehaviour
{
    public float floatSpeed = 20f;
    public float fadeSpeed = 0.4f;
    public float stayDuration = 0.4f;

    private TextMeshProUGUI text;
    private Color currentColor;
    private float timer = 0f;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string value, Color c)
    {
        if (text == null)
            text = GetComponent<TextMeshProUGUI>();

        text.text = value;
        currentColor = c;
        currentColor.a = 1f;   // fully visible
        text.color = currentColor;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // move upward
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);

        // start fading after stayDuration
        if (timer > stayDuration)
        {
            currentColor.a -= fadeSpeed * Time.deltaTime;
            text.color = currentColor;

            if (currentColor.a <= 0f)
                Destroy(gameObject);
        }
    }
}
