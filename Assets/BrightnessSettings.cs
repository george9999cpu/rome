using UnityEngine;
using UnityEngine.UI;

public class BrightnessSettings : MonoBehaviour
{
    public Image overlay1;   // First brightness overlay
    public Image overlay2;   // Second brightness overlay

    public void SetBrightness(float value)
    {
        SetOverlay(overlay1, value);
        SetOverlay(overlay2, value);
    }

    private void SetOverlay(Image img, float value)
    {
        if (img == null) return;

        Color c = img.color;
        c.a = 0.7f * (1f - value);   // same behaviour as original
        img.color = c;
    }
}
