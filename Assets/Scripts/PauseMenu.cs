using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseBG;
    public GameObject pausePanel;
    public GameObject pauseButton;


[Header("Player")]
    public PlayerHealth player;         // drag player here
    public PlayerShooting shooter; 

    
    [Header("Panels")]
    public GameUIController ui;
    public GameObject uiMainMenu;   
   public GameObject menuPanel;      // child: Main menu panel
    public GameObject optionsPanel;   // child: Options panel
    public GameObject gameplayPanel;   // if you have one (optional)
public GameObject deathPanel;
    [Header("Hide During Pause")]
    public GameObject ammoUI;         // ammo / HUD parent
public GameObject sliderUI;   // NEW — drag your slider parent here

      // your ship health script (optional)

    bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
if (sliderUI) sliderUI.SetActive(false);
        if (pauseBG)     pauseBG.SetActive(true);
        if (pausePanel)  pausePanel.SetActive(true);
        if (pauseButton) pauseButton.SetActive(false);
        if (ammoUI)      ammoUI.SetActive(false);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
if (sliderUI) sliderUI.SetActive(true);

        if (pauseBG)     pauseBG.SetActive(false);
        if (pausePanel)  pausePanel.SetActive(false);
        if (pauseButton) pauseButton.SetActive(true);
        if (ammoUI)      ammoUI.SetActive(true);
    }

    // ==========================
    //  RESTART BUTTON
    // ==========================
    public void RestartGame()
{



    if (deathPanel != null)
    deathPanel.SetActive(false);
    // unpause time
    Time.timeScale = 1f;
    isPaused = false;

    // close pause / death panels
    if (pauseBG)    pauseBG.SetActive(false);
    if (pausePanel) pausePanel.SetActive(false);
    if (deathPanel) deathPanel.SetActive(false);

    // make sure gameplay UI is active
    if (menuPanel)     menuPanel.SetActive(false);
    if (gameplayPanel) gameplayPanel.SetActive(true);
    if (ammoUI)        ammoUI.SetActive(true);
    if (pauseButton)   pauseButton.SetActive(true);
    if (sliderUI) sliderUI.SetActive(true);

    // reset player (hp + position)
    if (player != null)
    {
        player.ResetHealthAndPosition();

        // re-enable movement & shooting (they got disabled in Die())
        var move = player.GetComponent<PlayerController>();
        if (move != null) move.enabled = true;

        var shootComp = player.GetComponent<PlayerShooting>();
        if (shootComp != null) shootComp.enabled = true;
    }

    // reset all ammo types
    if (shooter != null)
    {
        shooter.ResetAmmo();
    }
}

    // ==========================
    //  MAIN MENU BUTTON
    // ==========================
 public void GoToMainMenu()
{
    Time.timeScale = 1f;
    isPaused = false;

    // --- RESET PLAYER ---
    if (player != null)
    {
        player.ResetHealthAndPosition();

        // re-enable movement & shooting (in case player died)
        var move = player.GetComponent<PlayerController>();
        if (move != null) move.enabled = true;

        var shootComp = player.GetComponent<PlayerShooting>();
        if (shootComp != null) shootComp.enabled = true;
    }

    // --- RESET AMMO ---
    if (shooter != null)
        shooter.ResetAmmo();

    // --- SWITCH UI BACK TO MAIN MENU ---
    if (ui != null)
        ui.OpenMainMenu();

    // --- HIDE PAUSE VISUALS ---
    if (pauseBG)     pauseBG.SetActive(false);
    if (pausePanel)  pausePanel.SetActive(false);
    if (pauseButton) pauseButton.SetActive(false);

    // --- SWITCH TO MENU MUSIC ---
    if (AudioManager.inst != null)
        AudioManager.inst.PlayMenuMusic();
}


    // your Quit button is probably something like:
    public void QuitGame()
    {
        Application.Quit();
    }
}
