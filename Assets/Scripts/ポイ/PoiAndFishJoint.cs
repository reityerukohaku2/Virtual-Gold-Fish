using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PoiAndFishJoint : MonoBehaviour
{
    List<FixedJoint> joints = new List<FixedJoint>();
    bool firstAvenger = false;

    private static PoiAndFishJoint instance;
    public static PoiAndFishJoint Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PoiAndFishJoint>();
            }
            return instance;
        }
    }

    //ジョイント中の金魚の数を返す
    public int JointNum => joints.Count;

    //未接続の場合接続する
    public FixedJoint AddJoint(Rigidbody other)
    {
        FixedJoint addJoint = new FixedJoint();
        bool existing = false;

        //ポイが生きてる時だけすくえる
        if (!BreakPoi.Instance.PoiIsDefeat)
        {
            //検索
            if (firstAvenger)
            {
                foreach (FixedJoint joint in joints)
                {
                    if (joint.connectedBody == other)
                    {
                        existing = true;
                        break;
                    }
                }
            }
            else
            {
                firstAvenger = true;
            }

            if (!existing)
            {
                addJoint = gameObject.AddComponent<FixedJoint>();
                addJoint.connectedBody = other;
                joints.Add(addJoint);
            }
            return addJoint;
        }
        else return null;
        
    }

    //金魚を全部逃がす
    public void Release()
    {
        if (joints.Count == 0) return;
        foreach (FixedJoint joint in joints)
        {
            joint.connectedBody.useGravity = true;

            //影をつける
            joint.connectedBody.GetComponentInChildren<SkinnedMeshRenderer>().shadowCastingMode = ShadowCastingMode.On;

            Destroy(joint);
        }

        joints = new List<FixedJoint>();
    }
}
