using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseBG;
    public GameObject pausePanel;
    public GameObject pauseButton;

    [Header("Hide During Pause")]
    public GameObject ammoUI;   // NEW ← your ammo display parent

    private bool isPaused = false;

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
        pauseBG.SetActive(true);
        pausePanel.SetActive(true);

        if (pauseButton != null)
            pauseButton.SetActive(false);

        if (ammoUI != null)
            ammoUI.SetActive(false);   // NEW ← hide ammo during pause

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseBG.SetActive(false);
        pausePanel.SetActive(false);

        if (pauseButton != null)
            pauseButton.SetActive(true);

        if (ammoUI != null)
            ammoUI.SetActive(true);    // NEW ← show ammo again

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
