using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    

    [Header("Teleport Values")]
    [SerializeField] private bool canTeleport;
    [SerializeField] private int lineResolution;
    [SerializeField] private Vector3 curveHeight;
    [SerializeField] private Transform reticle;
    [SerializeField] private RawImage fader;
    [SerializeField] private Color validTargetStart, validTargetEnd, invalidTargetStart, invalidTargetEnd;

    // Private variables
    private float verticalValue, horizontalValue;
    private RaycastHit hit;
    private LineRenderer line;
    private bool teleportLock = false;
    private bool isValidTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = lineResolution;

        fader.color = Color.clear;
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
            reticle.gameObject.SetActive(true);

            isValidTarget = hit.collider.CompareTag("Teleportable");

            if (isValidTarget)
            {
                // Teleport
                line.startColor = validTargetStart;
                line.endColor = validTargetEnd;
            }
            else
            {
                // Cannot teleport
                line.startColor = invalidTargetStart;
                line.endColor = invalidTargetEnd;
            }

            // Identify all the points

            Vector3 startPoint = transform.position;
            Vector3 endPoint = hit.point;
            Vector3 midPoint = ((endPoint - startPoint) / 2 + startPoint);  // (B-A)/2 + A
            midPoint += curveHeight; // 2,3,4  + 0,2,0 = 2,5,4

            // Smooth movement of the curve
            Vector3 desiredPosition = endPoint - reticle.position;
            Vector3 smoothVectorToDesired = (desiredPosition / 0.2f) * Time.deltaTime;
            Vector3 reticleEndpoint = reticle.position + smoothVectorToDesired;

            // Set the reticle position
            reticle.position = reticleEndpoint;
            reticle.up = hit.normal;


            // Apply the curve


            //line.SetPosition(0, transform.position);
            //line.SetPosition(1, hit.point);

            for(int i = 0; i<lineResolution; i++)
            {
                float t = i / (float)lineResolution; // 0, 1/20, 2/20, 3/20 ...... 19/20  >  0 - 1 // 0, 0.05, 0.1, 0.15, ..... 0.95

                Vector3 StartToMid = Vector3.Lerp(startPoint, midPoint, t);
                Vector3 MidToEnd = Vector3.Lerp(midPoint, reticleEndpoint, t);

                Vector3 curvePosition = Vector3.Lerp(StartToMid, MidToEnd, t);

                line.SetPosition(i, curvePosition);
            }


            if (Input.GetButtonDown(teleportTriggerButton)  && !teleportLock  && isValidTarget)
            {
                StartCoroutine(FadeTeleport(endPoint));
            }
        }
        else
        {
            line.enabled = false;
            reticle.gameObject.SetActive(false);
        }
    }

    IEnumerator FadeTeleport(Vector3 newPosition)
    {
        teleportLock = true;

        for(float time = 0; time < 1; time += Time.deltaTime*2)
        {
            fader.color = Color.Lerp(Color.clear, Color.black, time);
            yield return new WaitForEndOfFrame();
        }

        XRRig.position = newPosition;

        for (float time = 0; time < 1; time += Time.deltaTime*2)
        {
            fader.color = Color.Lerp(Color.black, Color.clear, time);
            yield return new WaitForEndOfFrame();
        }

        teleportLock = false;
    }

}
