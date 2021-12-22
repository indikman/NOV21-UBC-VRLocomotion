using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappableObject : GrabbableObject
{
    private Rigidbody body;

    private Transform socket;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public override void OnGrabEnd()
    {
        base.OnGrabEnd();

        if(socket != null)
        {
            body.useGravity = false;
            body.isKinematic = true;

            transform.position = socket.position;
            transform.rotation = socket.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("socket"))
        {
            socket = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("socket"))
        {
            socket = null;
        }
    }
}
