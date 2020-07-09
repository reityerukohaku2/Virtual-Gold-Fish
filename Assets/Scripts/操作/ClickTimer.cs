using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTimer : MonoBehaviour
{
    float startTime = 0;
    private float myTime = 0;
    public float MyTime => myTime;
    Touch touch;

    // Update is called once per frame
    void Update()
    {
        touch = TouchManager.TouchControl.Touch;
        if(touch.phase == TouchPhase.Began)
        {
            startTime = Time.time;
        }
        else
        {
            myTime = Time.time - startTime;
        }
    }
}
