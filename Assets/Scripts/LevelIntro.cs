using UnityEngine;
using TMPro;
using System.Collections;

public class LevelIntro : MonoBehaviour
{
    public TMP_Text introText;
    public float fadeInTime = 1f;
    public float displayTime = 2f;
    public float fadeOutTime = 1f;

    void Awake()
    {
        if (introText != null)
        {
            introText.gameObject.SetActive(false);   // hidden at start
        }
    }

    // Call this to show any level text
    public void Show(string text)
    {
        if (introText == null)
        {
            Debug.LogWarning("LevelIntro: introText is NOT assigned!");
            return;
        }

        introText.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(ShowIntro(text));
    }

    private IEnumerator ShowIntro(string text)
    {
        introText.text = text;

        Color c = introText.color;
        c.a = 0;
        introText.color = c;

        float t = 0;

        // fade in
        while (t < fadeInTime)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, t / fadeInTime);
            introText.color = c;
            yield return null;
        }

        // stay
        yield return new WaitForSeconds(displayTime);

        // fade out
        t = 0;
        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1, 0, t / fadeOutTime);
            introText.color = c;
            yield return null;
        }

        introText.gameObject.SetActive(false);
    }
}
