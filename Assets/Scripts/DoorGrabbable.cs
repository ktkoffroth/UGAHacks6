using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DoorGrabbable : Throwable
{

    public Transform handler;
    public GameObject frontHandler;
    protected override void OnDetachedFromHand(Hand hand)
    {
        base.OnDetachedFromHand(hand);

        frontHandler.GetComponent<FollowPhysics>().switchRoles = true;
        transform.position = handler.transform.position;
        //transform.rotation = handler.transform.rotation;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        rigidbody.isKinematic = false;
        transform.SetParent(handler);

    }

    protected override void OnAttachedToHand(Hand hand)
    {
        base.OnAttachedToHand(hand);
        rigidbody.isKinematic = true;
        frontHandler.GetComponent<FollowPhysics>().switchRoles = false;
    }
}
 