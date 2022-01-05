using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public bool offsetGrab;

    private MeshRenderer rend;
    public Color hoveredColor;

    private Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        defaultColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnHoverStart()
    {
        if(rend != null)   //  to avoid nullreference exception
        {
            rend.material.color = hoveredColor;
        }
        
    }

    public virtual void OnHoverEnd()
    {
        if(rend != null)
        {
            rend.material.color = defaultColor;
        }
       
    }

    public virtual void OnGrabStart(XRHand hand)
    {
        transform.SetParent(hand.transform);

        if (!offsetGrab)
        {
            transform.position = hand.transform.position;
            transform.rotation = hand.transform.rotation;
        }
        

        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public virtual void OnGrabEnd()
    {
        transform.SetParent(null);
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public virtual void OnTriggerStart()
    {

    }

    public virtual void OnTriggerEnd()
    {

    }

    public virtual void OnTrigger()
    {

    }
}
