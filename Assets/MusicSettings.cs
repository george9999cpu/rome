using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicSettings : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;

    void Start()
    {
        float current;
        mixer.GetFloat("MusicVolume", out current);
        musicSlider.value = current;
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("MusicVolume", value);
    }
}
