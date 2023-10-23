using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Send a message to the parent object to reverse its rotation
            transform.parent.SendMessage("ReverseRotation", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Send a message to the parent object to reverse its rotation
            transform.parent.SendMessage("ReverseRotation", SendMessageOptions.DontRequireReceiver);
        }
    }
}
