using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBucket : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "boidCollider")
        {
            //main.Instance.RemoveBoid(other.GetComponentInParent<boidsScript>());
            //other.GetComponentInParent<boidsScript>().enabled = false;
            //other.GetComponentInParent<boidsScript>().ProcessSwitch = true;
            FindObjectOfType<CGoldFishNum>().GetComponent<CGoldFishNum>().AddCount();
        }
    }
}
