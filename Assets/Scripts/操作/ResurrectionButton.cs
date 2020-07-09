using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResurrectionButton : MonoBehaviour
{
    private Button button;
    private Text text;

    void Start()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
    }

    //ポイが破れた時だけボタンが出現
    void Update()
    {
        if(BreakPoi.Instance.PoiIsDefeat)
        {
            button.image.enabled = true;
            button.enabled = true;
            text.enabled = true;
        }
        else
        {
            button.image.enabled = false;
            button.enabled = false;
            text.enabled = false;
        }
    }
}
