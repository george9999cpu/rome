using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager inst;

    [Header("Music Toggle")]
    public bool isMuted = false;
    public UnityEngine.UI.Image musicBtnImage;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    [Header("Audio Source")]
    public AudioSource musicSrc;

    [Header("Menu music (4 clips)")]
    public AudioClip[] menuClips;

    [Header("Gameplay music (4 clips)")]
    public AudioClip[] gameplayClips;

    [Header("Special Level 5 music")]
    public AudioClip level5Clip;

    int menuIndex = 0;
    int gameplayIndex = 0;


void Start()
{
    PlayMenuMusic();   // menu music as soon as the game starts
}
    void Awake()
    {
        if (inst != null && inst != this)
        {
            Destroy(gameObject);
            return;
        }

        inst = this;
        DontDestroyOnLoad(gameObject);

        if (musicSrc == null)
            musicSrc = GetComponent<AudioSource>();
    }

    // -----------------------------
    //  MANUAL SWITCH FUNCTIONS
    // -----------------------------

    public void PlayMenuMusic()
    {
        if (menuClips.Length == 0) return;
        PlayClip(menuClips[menuIndex]);
    }

    public void PlayGameplayMusic()
    {
        if (gameplayClips.Length == 0) return;
        PlayClip(gameplayClips[gameplayIndex]);
    }

    public void PlayLevel5Music()
    {
        if (level5Clip == null) return;
        PlayClip(level5Clip);
    }

    public void StopMusic()
    {
        musicSrc.Stop();
    }

    void PlayClip(AudioClip clip)
    {
        if (clip == null) return;
        musicSrc.clip = clip;
        musicSrc.loop = true;
        musicSrc.Play();
    }
public void OnMenuOpened()
{
    PlayMenuMusic();
}

public void OnStartGame()
{
    PlayGameplayMusic();
}
    // -----------------------------
    // UI CONTROL METHODS
    // -----------------------------

    public void ToggleMusic()
    {
        isMuted = !isMuted;
        musicSrc.mute = isMuted;

        if (musicBtnImage != null)
            musicBtnImage.sprite = isMuted ? musicOffSprite : musicOnSprite;
    }

    public void SetMenuIndex(int i)
    {
        menuIndex = Mathf.Clamp(i, 0, menuClips.Length - 1);
        PlayMenuMusic(); // immediate update
    }

    public void SetGameplayIndex(int i)
    {
        gameplayIndex = Mathf.Clamp(i, 0, gameplayClips.Length - 1);
        PlayGameplayMusic(); // immediate update
    }
}
