using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public PlayerShooting player;

    public TextMeshProUGUI arrowText;
    public TextMeshProUGUI cannonText;
    public TextMeshProUGUI fireText;

    // Gold color
    private readonly Color gold = new Color(1f, 0.84f, 0f);

    void Update()
    {
        if (player == null) return;

        // ARROW
        if (arrowText != null)
        {
            arrowText.text = "x" + player.arrowAmmo;

            if (player.arrowAmmo <= 0)
                arrowText.color = Color.gray;
            else if (player.arrowTimer > 0f)
                arrowText.color = Color.red;
            else
                arrowText.color = gold;
        }

        // CANNON
        if (cannonText != null)
        {
            cannonText.text = "x" + player.cannonAmmo;

            if (player.cannonAmmo <= 0)
                cannonText.color = Color.gray;
            else if (player.cannonTimer > 0f)
                cannonText.color = Color.red;
            else
                cannonText.color = gold;
        }

        // FIRE
        if (fireText != null)
        {
            fireText.text = "x" + player.fireAmmo;

            if (player.fireAmmo <= 0)
                fireText.color = Color.gray;
            else if (player.fireTimer > 0f)
                fireText.color = Color.red;
            else
                fireText.color = gold;
        }
    }
}
