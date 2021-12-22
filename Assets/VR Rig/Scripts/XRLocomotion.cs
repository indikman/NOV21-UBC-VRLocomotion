using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRLocomotion : MonoBehaviour
{
    [SerializeField] private Transform XRRig;
    [SerializeField] private Transform XRHead;

    [Header("Input Bindings")]
    [SerializeField] private string verticalAxis;
    [SerializeField] private string horizontalAxis;
    [SerializeField] private string teleportTriggerButton;

    [Header("Movement Values")]
    [SerializeField] private bool canSmoothMove;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool canSmoothRotate;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool canTeleport;

    // Private variables
    private float verticalValue, horizontalValue;
    private RaycastHit hit;
    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalValue = Input.GetAxis(verticalAxis);
        horizontalValue = Input.GetAxis(horizontalAxis);

        //Debug.Log(verticalValue + "   " + horizontalValue);
        if(canSmoothMove)
            SmoothMove(verticalValue);

        if (canSmoothRotate)
            SmoothRotate(horizontalValue);

        if (canTeleport)
            Teleport();
    }

    void SmoothMove(float axisValue)
    {
        Vector3 lookDirection = new Vector3(XRHead.forward.x, 0, XRHead.forward.z);
        lookDirection.Normalize();

        XRRig.position += lookDirection * Time.deltaTime * axisValue * -1 * moveSpeed;
    }

    void SmoothRotate(float axisValue)
    {
        XRRig.Rotate(Vector3.up, rotateSpeed * Time.deltaTime * axisValue);
    }

    void Teleport()
    {
        //Creata a new ray for casting
        Ray ray = new Ray(transform.position, transform.forward);

        if(Physics.Raycast(ray, out hit))
        {
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hit.point);

            if (Input.GetButtonDown(teleportTriggerButton))
            {
                XRRig.position = hit.point;
            }
        }
        else
        {
            line.enabled = false;
        }
    }
}
