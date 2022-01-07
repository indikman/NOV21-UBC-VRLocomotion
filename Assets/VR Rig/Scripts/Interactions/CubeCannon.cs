using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCannon : MonoBehaviour
{
    public GameObject cubePrefab;
    public float shootVelocity;

    public void ShootCube()
    {
        var cube = Instantiate(cubePrefab, transform.position, transform.rotation);
        cube.GetComponent<Rigidbody>().velocity = Vector3.up * shootVelocity;
    }
}
