using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource MusicSource;
    public AudioSource SoundEffectSource;
    public AudioMixer MainMixer;
    static GameObject instance;
    public List<AudioClip> AC = new List<AudioClip>();
    public int MainMenuSceneNumber;

    public float MainVolume = 0.5f;
    public float SoundEffectVolume = 0.5f;
    public float MusicVolume = 0.5f;


    void Awake()
    {
        if (instance) Destroy(gameObject);
        else
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        } 
    }
    private void Start()
    {
        MusicSource.Play();
    }

    public void PlayOneShot(string clipName, float Volume)
    {
        AudioClip clip = null;

        foreach (AudioClip c in AC)
        {
            if (c.name == clipName)
            {
                clip = c;
                break;
            }
        } 

        if (clip != null) SoundEffectSource.PlayOneShot(clip, Volume);
    }
}
