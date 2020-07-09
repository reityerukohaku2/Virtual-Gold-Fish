using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CPoiNum : CDrawCount
{
    public int count;

    void Start()
    {
        //count = 3;
        text = GetComponentInChildren<Text>();
        DrawCount(count);
    }

    //カウントを1減らす
    public void SubCount()
    {
        count--;
        DrawCount(count);
        if(count < 0)
        {
            //ゲームオーバー
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
