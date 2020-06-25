using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEffectOnClick : MonoBehaviour
{
    public string SoundEffectName = "ButtonClick";
    public float v = 0.35f;

    public void Clicked()
    {
        FindObjectOfType<AudioManager>().PlayOneShot(SoundEffectName, v);
    }
}
