using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRHand : MonoBehaviour
{
    public Animator handAnim;
    public string gripButton;

    private GrabbableObject hoveredObject;
    private GrabbableObject grabbedObject;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetButtonDown(gripButton))
        {
            // Grip!
            handAnim.SetBool("Gripped", true);

            if(hoveredObject != null)
            {
                grabbedObject = hoveredObject;
                hoveredObject.OnGrabStart(this);
                hoveredObject = null;
            }
            
        }

        if (Input.GetButtonUp(gripButton))
        {
            
            handAnim.SetBool("Gripped", false);

            // UnGrip!
            if(grabbedObject != null)
            {
                grabbedObject.OnGrabEnd();
                grabbedObject = null;
            }
            
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        var grabbable = other.GetComponent<GrabbableObject>();


        if(grabbable != null)
        {
            hoveredObject = grabbable;
            grabbable.OnHoverStart();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        var grabbable = other.GetComponent<GrabbableObject>();

        if (grabbable != null)
        {
            hoveredObject = null;
            grabbable.OnHoverEnd();
        }
    }
}
