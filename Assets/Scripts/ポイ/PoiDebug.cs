using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiDebug : MonoBehaviour
{
    private Rigidbody poiRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        poiRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(poiRigidbody.velocity);
    }
}
