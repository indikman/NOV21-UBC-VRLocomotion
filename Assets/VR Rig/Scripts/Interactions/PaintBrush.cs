using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBrush : ThrowableObject
{
    public GameObject paintPrefab;

    public Transform paintTip;
    public Renderer paintTipColor;



    private Material paintMaterial;
    private bool isColourSelected = false;

    private GameObject tempPaint;

    public override void OnTriggerStart()
    {
        tempPaint = Instantiate(paintPrefab, paintTip.position, paintTip.rotation);
       // paintPrefab.transform.SetParent(tempPaint.transform);

        tempPaint.GetComponent<Renderer>().material = paintMaterial;
    }

    public override void OnTrigger()
    {
        tempPaint.transform.position = paintTip.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paint"))
        {
            paintMaterial = other.GetComponent<Renderer>().material;
            paintTipColor.material = paintMaterial;
            isColourSelected = true;
        }
    }



}
