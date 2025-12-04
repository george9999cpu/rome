using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXToggle : MonoBehaviour
{
    public AudioMixer mixer;        // MasterMixer
    public Image sfxBtnImage;       // the button's Image
    public Sprite sfxOnSprite;
    public Sprite sfxOffSprite;

    bool isMuted = false;
    float lastVolume = 0f;          // remember previous SFX volume

    public void ToggleSFX()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            // store current SFX volume, then mute
            mixer.GetFloat("SFXVolume", out lastVolume);
            mixer.SetFloat("SFXVolume", -80f);
        }
        else
        {
            // restore previous volume
            mixer.SetFloat("SFXVolume", lastVolume);
        }

        // change button icon
        if (sfxBtnImage != null)
            sfxBtnImage.sprite = isMuted ? sfxOffSprite : sfxOnSprite;
    }
}
