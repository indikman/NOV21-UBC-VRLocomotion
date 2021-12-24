using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    public Transform pointA, pointB, pointC, pointX, pointY, pointP;

    public float lerpPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lerpPosition >= 1)
        {
            lerpPosition = 0;
        }
        else
        {
            lerpPosition += Time.deltaTime;
        }



        pointX.position = Vector3.Lerp(pointA.position, pointB.position, lerpPosition);
        pointY.position = Vector3.Lerp(pointB.position, pointC.position, lerpPosition);

        pointP.position = Vector3.Lerp(pointX.position, pointY.position, lerpPosition);
    }
}
