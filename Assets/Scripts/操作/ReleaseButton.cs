using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReleaseButton : MonoBehaviour
{
    private static float startTime;
    private static bool releasing;
    public float cycle, angle;

    void Start()
    {
        releasing = false;
    }

    private void Update()
    {
        if (releasing)
        {
            //角度「最大角度 * cos(πθ/周期(s))」
            transform.rotation = Quaternion.Euler(  transform.localEulerAngles.x, 
                                                    transform.localEulerAngles.y, 
                                                    angle * Mathf.Sin(Mathf.PI * (Time.time - startTime) / cycle));
            if(Time.time - startTime > cycle)
            {
                releasing = false;
            }
        }
    }

    //welcome to Goldfish
    public void OnReleaseButton()
    {
        if(!releasing)
        {
            releasing = true;
            startTime = Time.time;
            PoiAndFishJoint.Instance.Release();
        }
    }
}
