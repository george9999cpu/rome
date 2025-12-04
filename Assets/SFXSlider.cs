using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SFXSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    void Start()
    {
        float value;
        if (mixer.GetFloat("SFXVolume", out value))
            slider.value = value;
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFXVolume", value);
    }
}
