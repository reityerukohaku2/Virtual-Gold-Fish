using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsAudio : MonoBehaviour
{
    public AudioSource audioSource;

    private bool inWater = true;

    void Update()
    {
        if(inWater && transform.position.y >= main.Instance.tankMaxY)
        {
            inWater = false;
        }
        else if(!inWater && transform.position.y <= main.Instance.tankMaxY)
        {
            inWater = true;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
