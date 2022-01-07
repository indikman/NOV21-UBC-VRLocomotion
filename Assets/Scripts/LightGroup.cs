using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGroup : MonoBehaviour
{

    public GameObject[] lights;

    public void TurnLightsOn()
    {
        StartCoroutine(lightsOn());
    }

    public void TurnLightsOff()
    {
        StartCoroutine(lightsOff());
    }

    IEnumerator lightsOn()
    {
        foreach(GameObject i in lights)
        {
            i.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator lightsOff()
    {
        foreach (GameObject i in lights)
        {
            i.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
