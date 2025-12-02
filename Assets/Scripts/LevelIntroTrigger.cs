using UnityEngine;

public class LevelIntroTrigger : MonoBehaviour
{
    [TextArea]
    public string levelText = "Level text here";

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Ship"))   // your ship is tagged "Ship"
        {
            triggered = true;

            LevelIntro intro = FindObjectOfType<LevelIntro>();
            if (intro != null)
            {
                intro.Show(levelText);
            }
        }
    }
}
