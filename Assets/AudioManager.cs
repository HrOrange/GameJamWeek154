using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    AudioSource source;
    static GameObject instance;
    public List<AudioClip> AC = new List<AudioClip>();

    void Awake()
    {
        if (instance) Destroy(gameObject);
        else
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        } 
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void PlayOneShot(string clipName, float Volume)
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

        if (clip != null) source.PlayOneShot(clip, Volume);
    }
}
