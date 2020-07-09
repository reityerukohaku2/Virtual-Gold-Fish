using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CDrawCount : MonoBehaviour
{
    protected Text text;

    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    //countで指定した数を表示
    protected void DrawCount(int count)
    {
        text.text = "X " + count.ToString();
    }
}
