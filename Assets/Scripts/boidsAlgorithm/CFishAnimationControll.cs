using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFishAnimationControll : MonoBehaviour
{
    private Animator animator;
    private float MaxSpeed = 0.5f, NowSpeed;
    private Rigidbody thisRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Animator anim in GetComponentsInChildren<Animator>())
        {
            if(anim.gameObject.activeSelf)
            {
                //Debug.Log(anim.gameObject);
                animator = anim;
                break;
            }
        }

        NowSpeed = 0;
        thisRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        NowSpeed = thisRigidbody.velocity.magnitude;
        //Debug.Log(NowSpeed);
        animator.speed = NowSpeed / MaxSpeed;
    }
}
