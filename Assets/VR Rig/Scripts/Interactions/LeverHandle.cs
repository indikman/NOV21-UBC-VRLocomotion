using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHandle : ThrowableObject
{
    public Transform leverHandlePoint;
    public Rigidbody rb;
    public Rigidbody lever;
    public float maxDistanceToBreak = 0.5f;

    private FixedJoint leverJoint;


    public override void OnGrabStart(XRHand hand)
    {
        base.OnGrabStart(hand);

        leverJoint = gameObject.AddComponent<FixedJoint>();
        leverJoint.connectedBody = lever;
    }

    public override void OnGrabEnd()
    {
        base.OnGrabEnd();

        Destroy(leverJoint);

        rb.isKinematic = true;
        rb.useGravity = false;

        transform.position = leverHandlePoint.position;
        transform.rotation = leverHandlePoint.rotation;

    }

    private void Update()
    {
        var distance = Vector3.Distance(transform.position, leverHandlePoint.position);
        if(distance > maxDistanceToBreak)
        {
            OnGrabEnd();
        }

    }
}
