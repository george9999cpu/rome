using UnityEngine;

public class LevelIntroTrigger : MonoBehaviour
{
    [TextArea]
    public string levelText = "Level text here";

    bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        Debug.Log("Something entered Level1Trigger: " + other.name);

        if (other.CompareTag("Ship"))
        {
            Debug.Log("Ship entered Level1Trigger");
            triggered = true;

            LevelIntro intro = FindObjectOfType<LevelIntro>();
            if (intro != null)
            {
                Debug.Log("Found LevelIntro, showing text");
                intro.Show(levelText);
            }
            else
            {
                Debug.LogError("No LevelIntro found in scene!");
            }
        }
    }
}
