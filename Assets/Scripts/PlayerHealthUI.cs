using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Image fillImage;   // Slider fill image

    public float flashDuration = 0.2f;         // how long the red flash lasts

    private int lastHealth;
    private Color normalTextColor;
    private Color normalFillColor;
    private bool colorsInitialized = false;
    private float flashTimer = 0f;

    private void OnEnable()
    {
        PlayerHealth.OnHealthChanged += UpdateBar;
    }

    private void OnDisable()
    {
        PlayerHealth.OnHealthChanged -= UpdateBar;
    }

    private void UpdateBar(int current, int max)
    {
        // Lazy-initialize colors the first time this is called
        if (!colorsInitialized)
        {
            normalTextColor = healthText.color;
            normalFillColor = fillImage.color;
            colorsInitialized = true;
        }

        healthSlider.maxValue = max;
        healthSlider.value = current;

        healthText.text = current.ToString();

        // 🔥 If we took damage -> flash red
        if (current < lastHealth)
        {
            flashTimer = flashDuration;
            healthText.color = Color.red;
            fillImage.color = Color.red;
        }

        lastHealth = current;
    }

    private void Update()
    {
        // Handle fading back from red to normal
        if (flashTimer > 0f)
        {
            flashTimer -= Time.deltaTime;

            float t = 1f - (flashTimer / flashDuration);  // 0 → 1 over time

            healthText.color = Color.Lerp(Color.red, normalTextColor, t);
            fillImage.color = Color.Lerp(Color.red, normalFillColor, t);

            if (flashTimer <= 0f)
            {
                // hard reset at the end just in case
                healthText.color = normalTextColor;
                fillImage.color = normalFillColor;
            }
        }
    }

    private void Reset()
    {
        healthSlider = GetComponent<Slider>();
    }
}
