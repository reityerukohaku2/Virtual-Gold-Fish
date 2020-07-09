using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Abs(Mathf.Sin(Time.time * 1.5f)));
    }


}
