using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class otherAvoidance : MonoBehaviour
{
    private boidsScript parent;

    void OnTriggerEnter(Collider other)
    {
        if (parent != null)
        {
            parent.OtherAvoidance(other);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (parent != null)
        {
            parent.OtherAvoidance(other);
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    parent.OtherAvoidance(collision);
    //}
    //void OnCollisionStay(Collision collision)
    //{
    //    parent.OtherAvoidance(collision);
    //}

    void Start()
    {
        parent = GetComponentInParent<boidsScript>();
    }
}
