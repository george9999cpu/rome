using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{


    [Header("Music Toggle")]
public bool isMuted = false;
public UnityEngine.UI.Image musicBtnImage;
public Sprite musicOnSprite;
public Sprite musicOffSprite;
    public static AudioManager inst;

    [Header("Audio Source")]
    public AudioSource musicSrc;

    [Header("Menu music (4 clips)")]
    public AudioClip[] menuClips;      // size 4

    [Header("Gameplay music (4 clips)")]
    public AudioClip[] gameplayClips;  // size 4

    [Header("Special level 5 music")]
    public AudioClip level5Clip;
    public string level5SceneName = "Level5";   // change to your real scene name

    int menuIndex = 0;
    int gameplayIndex = 0;

    void Awake()
    {
        // simple singleton
        if (inst != null && inst != this)
        {
            Destroy(gameObject);
            return;
        }

        inst = this;
        DontDestroyOnLoad(gameObject);

        // if not assigned in Inspector, grab the one on this object
        if (musicSrc == null)
            musicSrc = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // level 5 always uses scary track
        if (scene.name == level5SceneName)
        {
            PlayLevel5();
        }
        // main menu music
        else if (scene.name == "MainMenu")   // change to your menu scene name
        {
            PlayMenu();
        }
        // all other levels = normal gameplay music
        else
        {
            PlayGameplay();
        }
    }
public void ToggleMusic()
{
    isMuted = !isMuted;

    // Mute / unmute
    musicSrc.mute = isMuted;

    // Change button icon
    if (musicBtnImage != null)
        musicBtnImage.sprite = isMuted ? musicOffSprite : musicOnSprite;
}

    void PlayMenu()
    {
        if (menuClips.Length == 0) return;

        int i = Mathf.Clamp(menuIndex, 0, menuClips.Length - 1);
        PlayClip(menuClips[i]);
    }

    void PlayGameplay()
    {
        if (gameplayClips.Length == 0) return;

        int i = Mathf.Clamp(gameplayIndex, 0, gameplayClips.Length - 1);
        PlayClip(gameplayClips[i]);
    }

    void PlayLevel5()
    {
        if (level5Clip == null) return;
        PlayClip(level5Clip);
    }

    void PlayClip(AudioClip clip)
    {
        if (clip == null || musicSrc == null) return;

        musicSrc.clip = clip;
        musicSrc.loop = true;
        musicSrc.Play();
    }

    // called from Options menu (we’ll hook this later)
    public void SetMenuIndex(int i)
    {
        menuIndex = Mathf.Clamp(i, 0, menuClips.Length - 1);

        // if we’re currently in MainMenu, update immediately
        Scene s = SceneManager.GetActiveScene();
        if (s.name == "MainMenu")
            PlayMenu();
    }

    public void SetGameplayIndex(int i)
    {
        gameplayIndex = Mathf.Clamp(i, 0, gameplayClips.Length - 1);

        // if we’re in a gameplay scene (but not level 5), update
        Scene s = SceneManager.GetActiveScene();
        if (s.name != "MainMenu" && s.name != level5SceneName)
            PlayGameplay();
    }
}
