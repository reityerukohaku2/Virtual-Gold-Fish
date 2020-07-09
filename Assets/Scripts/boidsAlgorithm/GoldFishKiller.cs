using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishKiller : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Body"))
        {
            main.Instance.RemoveBoid(other.gameObject.transform.parent.gameObject.GetComponent<boidsScript>());     //リストからboidを消す
            Destroy(other.gameObject.transform.parent.gameObject);                                                  //死
            Debug.Log(other.gameObject.layer);
        }
    }
}
