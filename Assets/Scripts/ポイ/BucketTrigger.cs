using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketTrigger : MonoBehaviour
{
    private ReleaseButton releaseButton;

    private void Start()
    {
        releaseButton = FindObjectOfType<ReleaseButton>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (PoiAndFishJoint.Instance.JointNum > 0 && other.gameObject.layer == LayerMask.NameToLayer("POI"))
        {
            //ReleaseButtonVisible.Instance.Visible = true;
            releaseButton.OnReleaseButton();
        }
    }

    /*
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("POI"))
        {
            ReleaseButtonVisible.Instance.Visible = false;
        }
    }*/
}
