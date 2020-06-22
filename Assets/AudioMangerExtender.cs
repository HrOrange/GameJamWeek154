using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioMangerExtender : MonoBehaviour
{
    public TextMeshProUGUI MainVolumeShowText;
    public Slider MainVolumeSlider;
    public TextMeshProUGUI MusicVolumeShowText;
    public Slider MusicVolumeSlider;
    public TextMeshProUGUI SoundEffectVolumeShowText;
    public Slider SoundEffectVolumeSlider;
    public AudioManager AM;

    private void Start()
    {
        AM = FindObjectOfType<AudioManager>();
        AM.MainMixer.SetFloat("Main", AM.MainVolume);
        AM.MainMixer.SetFloat("Effect", AM.SoundEffectVolume);
        AM.MainMixer.SetFloat("Music", AM.MusicVolume);
    }

    public void OnMainVolumeChange(float newMainVolume)
    {
        if(newMainVolume != -20)
        {
            AM.MainVolume = Mathf.Round(newMainVolume * 100) / 100;
            MainVolumeShowText.text = ((AM.MainVolume + 20) / 40 * 100).ToString("F0") + "%";
            MainVolumeSlider.value = AM.MainVolume;
            AM.MainMixer.SetFloat("Main", AM.MainVolume);
        }
        else
        {
            AM.MainVolume = -80;
            MainVolumeShowText.text = "0%";
            MainVolumeSlider.value = -20;
            AM.MainMixer.SetFloat("Main", -80);
        }
    }

    public void OnSoundEffectVolumeChange(float newSoundEffectVolume)
    {
        if (newSoundEffectVolume != -20)
        {
            AM.SoundEffectVolume = Mathf.Round(newSoundEffectVolume * 100) / 100;
            SoundEffectVolumeShowText.text = ((AM.SoundEffectVolume + 20) / 40 * 100).ToString("F0") + "%";
            SoundEffectVolumeSlider.value = AM.SoundEffectVolume;
            AM.MainMixer.SetFloat("Effect", AM.SoundEffectVolume);
        }
        else
        {
            AM.SoundEffectVolume = -80;
            SoundEffectVolumeShowText.text = "0%";
            SoundEffectVolumeSlider.value = -20;
            AM.MainMixer.SetFloat("Effect", -80);
        }
    }

    public void OnMusicVolumeChange(float newMusicVolume)
    {
        if (newMusicVolume != -20)
        {
            AM.MusicVolume = Mathf.Round(newMusicVolume * 100) / 100;
            MusicVolumeShowText.text = ((AM.MusicVolume + 20) / 40 * 100).ToString("F0") + "%";
            MusicVolumeSlider.value = AM.MusicVolume;
            AM.MainMixer.SetFloat("Music", Mathf.Log10(AM.MusicVolume) * 20);
        }
        else
        {
            AM.MusicVolume = -80;
            MusicVolumeShowText.text = "0%";
            MusicVolumeSlider.value = -20;
            AM.MainMixer.SetFloat("Music", -80);
        }
    }
}
