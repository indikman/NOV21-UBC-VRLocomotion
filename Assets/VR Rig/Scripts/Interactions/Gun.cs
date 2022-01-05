using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : ThrowableObject
{
    public GameObject bulletObject;
    public Transform bulletSpawnPoint;
    public float shootForce;

    public AudioSource audioSource;
    public AudioClip shootSound;

    public override void OnTriggerStart()
    {
        // Instantiate a new bullet, and shoot it
        var bullet = Instantiate(bulletObject, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        bullet.GetComponent<Rigidbody>().AddForce(shootForce * bulletSpawnPoint.forward);
        Destroy(bullet, 5);

        if(shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

    }
}
