using System.Collections;
using UnityEngine;

public class DoubleDoorController : MonoBehaviour, IInteractable
{
    public DoorController Door1;
    public DoorController Door2;

    public string GetInteractText()
    {
        return Door1.GetInteractText(); 
    }

    public void Interact()
    {
        Door1.Interact();
        Door2.Interact();
    }
}
