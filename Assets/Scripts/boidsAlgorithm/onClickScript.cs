using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClickScript : MonoBehaviour
{
    public Rigidbody rigidbody;
    private boidsScript rootScript;

    void Start()
    {
        rootScript = transform.root.gameObject.GetComponent<boidsScript>();
    }

    public void onClick()
    {
        rootScript.ProcessSwitch = false;
        rigidbody.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
        Debug.Log("とおった");
    }
}
