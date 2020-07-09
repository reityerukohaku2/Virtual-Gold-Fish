using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tap : MonoBehaviour
{
    public void PushStart()
    {
        FadeManager.Instance.LoadScene("gameMain", 1.0f);
    }
}
