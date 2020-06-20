using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMangerExtender : MonoBehaviour
{
    public Text MainVolumeShowText;
    public Slider MainVolumeSlider;
    public Text MusicVolumeShowText;
    public Slider MusicVolumeSlider;
    public Text SoundEffectVolumeShowText;
    public Slider SoundEffectVolumeSlider;
    public AudioManager AM;

    private void Start()
    {
        AM = FindObjectOfType<AudioManager>();
        MainVolumeSlider.value = AM.MainVolume;
        MainVolumeShowText.text = (AM.MainVolume * 100).ToString("F0") + "%";
        MusicVolumeSlider.value = AM.MusicVolume;
        MusicVolumeShowText.text = (AM.MusicVolume * 100).ToString("F0") + "%";
        SoundEffectVolumeSlider.value = AM.SoundEffectVolume;
        SoundEffectVolumeShowText.text = (AM.SoundEffectVolume * 100).ToString("F0") + "%";
        AM.MainMixer.SetFloat("Main", AM.MainVolume * 20 - 10);
        AM.MainMixer.SetFloat("Effect", AM.SoundEffectVolume * 20 - 10);
        AM.MainMixer.SetFloat("Music", AM.MusicVolume * 20 - 10);
    }

    public void OnMainVolumeChange(float newMainVolume)
    {
        AM.MainVolume = Mathf.Round(newMainVolume * 100) / 100;
        MainVolumeShowText.text = (AM.MainVolume * 100).ToString("F0") + "%";
        MainVolumeSlider.value = AM.MainVolume;
        AM.MainMixer.SetFloat("Main", AM.MainVolume * 20 - 10);
    }

    public void OnSoundEffectVolumeChange(float newSoundEffectVolume)
    {
        AM.SoundEffectVolume = Mathf.Round(newSoundEffectVolume * 100) / 100;
        SoundEffectVolumeShowText.text = (AM.SoundEffectVolume * 100).ToString("F0") + "%";
        SoundEffectVolumeSlider.value = AM.SoundEffectVolume;
        AM.MainMixer.SetFloat("Effect", AM.SoundEffectVolume * 20 - 10);
    }

    public void OnMusicVolumeChange(float newMusicVolume)
    {
        AM.MusicVolume = Mathf.Round(newMusicVolume * 100) / 100;
        MusicVolumeShowText.text = (AM.MusicVolume * 100).ToString("F0") + "%";
        MusicVolumeSlider.value = AM.MusicVolume;
        AM.MainMixer.SetFloat("Music", AM.MusicVolume * 20 - 10);
    }
}
