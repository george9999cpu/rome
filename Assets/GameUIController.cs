using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [Header("Main Panels")]
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;

    [Header("Intro / Gameplay")]
    public GameObject introPanel1;
    public GameObject[] otherIntroPanels;
    public GameObject gameplayPanel;     // parent for in-game UI

    [Header("HUD / Stats")]
    public GameObject statsUI;           // AlwaysShowing
    public GameObject ammoUI;            // child under AlwaysShowing
    public GameObject pauseButton;       // child under AlwaysShowing

    // ----------------------------
    public void OpenOptions()
    {
        if (mainMenuPanel)  mainMenuPanel.SetActive(false);
        if (optionsPanel)   optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        if (optionsPanel)   optionsPanel.SetActive(false);
        if (mainMenuPanel)  mainMenuPanel.SetActive(true);
    }

    // ----------------------------
    // Start → show intro 1
    // ----------------------------
    public void StartGameplay()
    {
        Time.timeScale = 0f;

        if (mainMenuPanel)  mainMenuPanel.SetActive(false);
        if (optionsPanel)   optionsPanel.SetActive(false);

        if (gameplayPanel)  gameplayPanel.SetActive(false);
        if (statsUI)        statsUI.SetActive(false);
        if (ammoUI)         ammoUI.SetActive(false);
        if (pauseButton)    pauseButton.SetActive(false);

        ResetIntroPanels();
        if (introPanel1)    introPanel1.SetActive(true);

        if (AudioManager.inst != null)
            AudioManager.inst.PlayGameplayMusic();
    }

    // ----------------------------
    // Intro "Continue" button
    // ----------------------------
    public void ContinueFromIntro()
    {
        ResetIntroPanels();

        // show gameplay + HUD
        if (gameplayPanel)  gameplayPanel.SetActive(true);
        if (statsUI)        statsUI.SetActive(true);
        if (ammoUI)         ammoUI.SetActive(true);     // <-- FORCE ON
        if (pauseButton)    pauseButton.SetActive(true); // <-- FORCE ON

        Time.timeScale = 1f;
    }

    // ----------------------------
    // Main menu (from pause, etc.)
    // ----------------------------
    public void OpenMainMenu()
    {
        Time.timeScale = 1f;

        if (mainMenuPanel)  mainMenuPanel.SetActive(true);
        if (optionsPanel)   optionsPanel.SetActive(false);

        ResetIntroPanels();

        if (gameplayPanel)  gameplayPanel.SetActive(false);
        if (statsUI)        statsUI.SetActive(false);
        if (ammoUI)         ammoUI.SetActive(false);
        if (pauseButton)    pauseButton.SetActive(false);

        if (AudioManager.inst != null)
            AudioManager.inst.PlayMenuMusic();
    }

    void ResetIntroPanels()
    {
        if (introPanel1) introPanel1.SetActive(false);

        if (otherIntroPanels != null)
        {
            for (int i = 0; i < otherIntroPanels.Length; i++)
                if (otherIntroPanels[i])
                    otherIntroPanels[i].SetActive(false);
        }
    }

    public void QuitApp()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
