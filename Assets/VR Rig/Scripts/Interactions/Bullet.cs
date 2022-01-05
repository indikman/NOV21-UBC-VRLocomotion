using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletParticle;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(bulletParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    
}
