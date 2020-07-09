using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsSplash : MonoBehaviour
{
    private Splash splash;
    private bool inWater = true;

    private void Start()
    {
        splash = FindObjectOfType<Splash>().GetComponent<Splash>();
    }

    void Update()
    {
        if (inWater && transform.position.y >= main.Instance.tankMaxY)
        {
            inWater = false;
        }
        else if (!inWater && transform.position.y <= main.Instance.tankMaxY)
        {
            inWater = true;
            splash.PlaySplash(transform.position);
        }
    }
}
