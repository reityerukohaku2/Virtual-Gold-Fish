using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsInWater : MonoBehaviour
{
    private bool inWater = true;
    public AudioSource audioSource;
    private Splash splash;
    private CGenerateRipple ripple;

    private void Start()
    {
        ripple = FindObjectOfType<CGenerateRipple>();
        splash = FindObjectOfType<Splash>().GetComponent<Splash>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inWater && transform.position.y >= main.Instance.tankMaxY)
        {
            inWater = false;
        }
        else if (!inWater && transform.position.y <= main.Instance.tankMaxY)
        {
            inWater = true;
            audioSource.PlayOneShot(audioSource.clip);
            splash.PlaySplash(transform.position);
            ripple.GenerateRipple(new Vector2(transform.position.x, transform.position.z));
        }
    }
}
