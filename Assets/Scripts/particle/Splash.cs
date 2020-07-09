using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    List<ParticleSystem> splashes;
    public ParticleSystem splash;

    public void PlaySplash(Vector3 position)
    {
        position.y = main.Instance.tankMaxY;
        ParticleSystem particle = Instantiate(splash, position, splash.transform.rotation);
        splashes.Add(particle);
        particle.Play();
    }

    private void Start()
    {
        splashes = new List<ParticleSystem>();
    }

    private void Update()
    {
        if (splashes.Count > 0)
        {
            foreach (ParticleSystem splash in splashes)
            {
                if (!splash.isPlaying)
                {
                    splashes.Remove(splash);
                    Destroy(splash.gameObject);
                    break;
                }
            }
        }
    }
}
