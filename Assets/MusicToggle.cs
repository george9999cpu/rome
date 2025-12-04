using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public AudioMixer mixer;          // MasterMixer
    public Image musicBtnImage;       // the button's Image
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    // SHARED state for all MusicToggle buttons
    private static bool isMuted = false;
    private static float lastVolume = 0f;
    private const string MUSIC_PARAM = "MusicVolume";

    void Start()
    {
        SyncFromMixer();
        UpdateIcon();
    }

    void OnEnable()
    {
        // In case panel was hidden then shown again
        SyncFromMixer();
        UpdateIcon();
    }

    public void ToggleMusic()
    {
        float currentDb;
        mixer.GetFloat(MUSIC_PARAM, out currentDb);

        bool currentlyMuted = currentDb <= -79f;   // treat -80dB as muted

        if (!currentlyMuted)
        {
            // We are currently playing -> mute
            lastVolume = currentDb;               // remember real volume
            mixer.SetFloat(MUSIC_PARAM, -80f);
            isMuted = true;
        }
        else
        {
            // We are currently muted -> restore
            mixer.SetFloat(MUSIC_PARAM, lastVolume);
            isMuted = false;
        }

        UpdateIcon();
    }

    void SyncFromMixer()
    {
        float currentDb;
        if (mixer.GetFloat(MUSIC_PARAM, out currentDb))
        {
            bool mutedNow = currentDb <= -79f;
            isMuted = mutedNow;

            if (!mutedNow)
            {
                // keep track of the last "normal" volume
                lastVolume = currentDb;
            }
        }
    }

    void UpdateIcon()
    {
        if (musicBtnImage == null) return;

        musicBtnImage.sprite = isMuted ? musicOffSprite : musicOnSprite;
    }
}
