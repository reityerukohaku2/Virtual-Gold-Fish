using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishStatus : MonoBehaviour
{
    private Vector3 scale;      //金魚の大きさ
    private GameObject fishMesh;

    public FishStatus():this(Vector3.zero, null){}

    public FishStatus(Vector3 scale, GameObject fishMesh)
    {
        this.scale = scale;
        this.fishMesh = fishMesh;
    }

    public void OnFish(Transform transform)
    {
        transform.localScale = scale;
        fishMesh.SetActive(true);
    }
}
