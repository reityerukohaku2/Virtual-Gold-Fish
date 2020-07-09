using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReleaseButtonVisible : MonoBehaviour
{
    private Image image;
    private Button button;
    private Text text;

    private static ReleaseButtonVisible instance;
    public static ReleaseButtonVisible Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ReleaseButtonVisible>();
                //DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    //ボタンの可視状態
    private bool visible;
    public bool Visible
    {
        get => visible;
        set
        {
            visible = value;
            image.enabled = value;
            button.enabled = value;
            text.enabled = value;
        }
    }

    void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
    }
}
