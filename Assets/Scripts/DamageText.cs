using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float moveUpSpeed = 1f;
    public float fadeSpeed = 1f;

    private TMP_Text text;
    private CanvasGroup group;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        group = GetComponent<CanvasGroup>();
    }

    public void Setup(int dmg)
    {
        text.text = dmg.ToString();
    }

    void Update()
    {
        // float-up
        transform.position += Vector3.up * moveUpSpeed * Time.deltaTime;

        // fade out
        group.alpha -= fadeSpeed * Time.deltaTime;

        if (group.alpha <= 0)
            Destroy(gameObject);
    }
}
