using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FishOnPoi : MonoBehaviour
{
    static UpDownButton button;

    void Start()
    {
        button = GetComponentInParent<UpDownButton>();
    }

    //ポイに触れた金魚のBoidsAlgorithmを切る
    private void OnCollisionEnter(Collision other)
    {
        if (button.GoUp && !BreakPoi.Instance.PoiIsDefeat)
        {
            //衝突したオブジェクト
            GameObject hitObj = other.collider.gameObject;

            if (other.collider.tag == "boidCollider")
            {
                boidsScript boid = hitObj.GetComponentInParent<boidsScript>();
                boid.ProcessSwitch = false;     //BoidAlgorithmを切る

                PoiAndFishJoint.Instance.AddJoint(other.rigidbody);
                //BreakPoi.Instance.CheckBreakPoi();

                //影を消す
                hitObj.transform.parent.GetComponentInChildren<SkinnedMeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;

                //レイヤーを死体に変更
                hitObj.transform.parent.gameObject.layer = LayerMask.NameToLayer("Body");

                foreach (Transform transform in hitObj.transform.parent)
                {
                    transform.gameObject.layer = LayerMask.NameToLayer("Body");
                }
                
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (button.GoUp && !BreakPoi.Instance.PoiIsDefeat)
        {
            //衝突したオブジェクト
            GameObject hitObj = other.collider.gameObject;

            if (other.collider.tag == "boidCollider")
            {
                boidsScript boid = hitObj.GetComponentInParent<boidsScript>();
                boid.ProcessSwitch = false;     //BoidAlgorithmを切る

                PoiAndFishJoint.Instance.AddJoint(other.rigidbody);
                //BreakPoi.Instance.CheckBreakPoi();

                //影を消す
                hitObj.transform.parent.GetComponentInChildren<SkinnedMeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;

                //レイヤーを死体に変更
                hitObj.transform.parent.gameObject.layer = LayerMask.NameToLayer("Body");

                foreach (Transform transform in hitObj.transform.parent)
                {
                    transform.gameObject.layer = LayerMask.NameToLayer("Body");
                }

            }
        }
    }
}
