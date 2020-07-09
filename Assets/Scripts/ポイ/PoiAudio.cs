using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiAudio : MonoBehaviour
{
    public AudioSource[] audioSources = new AudioSource[2];

    //入水の音～
    public void PlayWaterEnter()
    {
        audioSources[0].PlayOneShot(audioSources[0].clip);
    }

    //水から出るときの音～
    public void PlayWaterFlood()
    {
        audioSources[1].PlayOneShot(audioSources[1].clip);
    }
}
