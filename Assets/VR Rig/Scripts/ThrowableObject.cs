using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : GrabbableObject
{
    public GrabType Type;

    public int numVelocitySamples = 10;
    public float throwBoost = 200;

    private Queue<Vector3> previousVelocities = new Queue<Vector3>(); // Defining a blank queue
    private Vector3 previousPosition;

    private Transform handTransform;
    private FixedJoint joint;

    public enum GrabType
    {
        Kinematic,
        JointBased
    }

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnHoverStart()
    {
       base.OnHoverStart();
    }

    public override void OnHoverEnd()
    {
        base.OnHoverEnd();
    }

    public override void OnGrabStart(XRHand hand)
    {
        if(Type == GrabType.Kinematic)
        {
            base.OnGrabStart(hand); // Kinematic way
        }
        else if(Type == GrabType.JointBased)
        {
            // Joint way
            joint = gameObject.AddComponent<FixedJoint>(); // Adding a new fixed joint component to the grabbable object

            joint.connectedBody = hand.GetComponent<Rigidbody>();
        }
         

         handTransform = hand.transform;
    }

    public override void OnGrabEnd()
    {
        if (Type == GrabType.Kinematic)
        {
            base.OnGrabEnd();
             // Kinematic way
        }
        else if (Type == GrabType.JointBased)
        {
            // Joint way
            Destroy(joint);
        }


        // Shoot the object
        // GetComponent<Rigidbody>().AddForce(shootForce * handTransform.forward); // Shooting the object instead of throwing


        // Calculate the average velocity and apply that to the object
        var averageVelocity = Vector3.zero;

        foreach(var tempVelocity in previousVelocities)
        {
            averageVelocity += tempVelocity;  // averageVelocity = averageVelocity + tempVelocity
        }

        averageVelocity /= previousVelocities.Count;  // actual average velocity

        // Apply the velocity

        GetComponent<Rigidbody>().velocity = averageVelocity * throwBoost;


    }


    private void FixedUpdate()
    {
        var velocity = transform.position - previousPosition; // Assume that the veocity = distance between positions

        previousPosition = transform.position;

        previousVelocities.Enqueue(velocity);

        if(previousVelocities.Count > numVelocitySamples)
        {
            previousVelocities.Dequeue();
        }
    }

}
