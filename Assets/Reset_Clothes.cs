using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset_Clothes : MonoBehaviour
{
    public GameObject clothes_parent;
    private List<Cloth> clothes = new List<Cloth>();

    //すべてのClothのClothコンポーネントをリストに格納
    void Start()
    {
        foreach(Transform clothes_children in clothes_parent.transform)
        {
            clothes.Add(clothes_children.GetComponent<Cloth>());
        }
    }

    //すべてのClothコンポーネントをリセット
    public void onClick()
    {
        foreach(Cloth cloth in clothes)
        {
            cloth.enabled = !cloth.enabled;
            cloth.enabled = !cloth.enabled;
        }
    }
}
