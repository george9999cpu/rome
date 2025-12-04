using UnityEngine;

public class MissionPanelsManager : MonoBehaviour
{
    [Header("Intro Panels in order (NO gameplay panel here)")]
    public GameObject[] panels;      // ONLY story / mission intro panels

    [Header("HUD Group (always showing UI)")]
    public GameObject hudGroup;      // e.g. "AlwaysShowing"

    [Header("Gameplay UI Elements")]
public GameObject ammoUI;
public GameObject pauseButton;
public GameObject slider;

    [Header("Gameplay Panel")]
    public GameObject gameplayPanel; // the last panel where you set stats

[Header("Player References")]
public PlayerHealth player;
public PlayerShooting shooter;



public GameObject deathpanel;
    private int index = 0;

    void Start()
    {
        BeginIntro();   // auto start when scene loads
    }

    public void BeginIntro()
    {
        Time.timeScale = 0f;   // pause game

        if (hudGroup != null)
            hudGroup.SetActive(false);   // hide HUD during intro

        if (gameplayPanel != null)
            gameplayPanel.SetActive(false); // also hide gameplay panel at start

        index = 0;             // always start at first panel
        ShowPanel(index);
    }

    public void NextPanel()
    {
        index++;

        // if we've gone past the last INTRO panel → start gameplay
        if (index >= panels.Length)
        {
            StartGameplay();
            return;
        }

        ShowPanel(index);
    }

    private void ShowPanel(int i)
    {
        // hide all INTRO panels
        foreach (GameObject p in panels)
            if (p != null)
                p.SetActive(false);

        // show current INTRO panel
        if (panels[i] != null)
            panels[i].SetActive(true);
    }

   public void StartGameplay()
{
    // hide intro panels
    foreach (GameObject p in panels)
        if (p != null)
            p.SetActive(false);

    // show HUD group
    if (hudGroup != null)
        hudGroup.SetActive(true);

    // show gameplay stats panel
    if (gameplayPanel != null)
        gameplayPanel.SetActive(true);

    // hide death panel (just to be safe)
    if (deathpanel != null)
        deathpanel.SetActive(false);

    // show ammo UI
    if (ammoUI != null)
        ammoUI.SetActive(true);

    // show pause button
    if (pauseButton != null)
        pauseButton.SetActive(true);

    // show health slider
    if (slider != null)
        slider.SetActive(true);

    // --- RESET HEALTH ---
    if (player != null)
    {
        player.ResetHealthAndPosition();

        var move = player.GetComponent<PlayerController>();
        if (move != null) move.enabled = true;

        var shootComp = player.GetComponent<PlayerShooting>();
        if (shootComp != null) shootComp.enabled = true;
    }

    // --- RESET AMMO ---
    if (shooter != null)
        shooter.ResetAmmo();

    // unpause game
    Time.timeScale = 1f;

    index = 0;
}


    public void PreviousPanel()
    {
        index--;

        if (index < 0)
        {
            index = 0;
            return;
        }

        ShowPanel(index);
    }
}
