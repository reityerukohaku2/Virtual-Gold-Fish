using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 爆破 : MonoBehaviour
{
    private static Rigidbody[] childrens = null;
    private static Cloth[] cloths;
    public float PowMax;

    // Start is called before the first frame update
    void Start()
    {
        childrens = GetComponentsInChildren<Rigidbody>();
        cloths = GetComponentsInChildren<Cloth>();
    }

    //爆破ボタン
    public void OnClickButton()
    {
        for(int i = 0; i < childrens.Length; i++)
        {
            //childrens[i].isKinematic = false;
            //childrens[i].AddForce(new Vector3(Random.Range(-PowMax, PowMax), Random.Range(-PowMax, PowMax), Random.Range(-PowMax, PowMax)));
            //childrens[i].useGravity = true;

            cloths[i].useGravity = true;
            cloths[i].externalAcceleration = new Vector3(Random.Range(-PowMax, PowMax), Random.Range(-PowMax, PowMax), Random.Range(-PowMax, PowMax));

            Debug.Log("通った");
        }
    }
}
