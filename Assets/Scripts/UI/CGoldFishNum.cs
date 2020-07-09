using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CGoldFishNum : CDrawCount
{
    private int count;

    void Start()
    {
        count = 0;
        text = GetComponentInChildren<Text>();
        DrawCount(count);
    }

    //カウントを1減らす
    public void AddCount()
    {
        count++;
        DrawCount(count);
    }
}
