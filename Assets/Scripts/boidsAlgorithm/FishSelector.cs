using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSelector : MonoBehaviour
{
    public GameObject normalMesh, telescopeMesh, tosakintMesh, otherAvoidance;
    FishStatus normal, telescope, tosakint;

    // Start is called before the first frame update
    void Start()
    {

        normal = new FishStatus(new Vector3(0.25f, 0.25f, 0.25f), normalMesh);
        telescope = new FishStatus(new Vector3(0.25f, 0.25f, 0.25f), telescopeMesh);
        tosakint = new FishStatus(new Vector3(0.5f, 0.5f, 0.5f), tosakintMesh);

        float rand = Random.value;

        if (rand >= 0.9f)
        {
            telescope.OnFish(transform);
        }
        else if(rand >= 0.85f)
        {
            tosakint.OnFish(transform);
            //otherAvoidance.SetActive(false);
        }
        else
        {
            normal.OnFish(transform);
        }
    }
}
