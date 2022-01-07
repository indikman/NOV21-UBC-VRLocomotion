using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour
{
    public Transform buttonHead; // button
    public Transform buttonUp; // position up
    public Transform buttonDown; // position down

    public UnityEvent OnPressed;
    public UnityEvent OnReleased;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            //Push the button
            buttonHead.position = buttonDown.position;

            OnPressed?.Invoke();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            //Push the button
            buttonHead.position = buttonUp.position;

            OnReleased?.Invoke();
        }
    }
}
