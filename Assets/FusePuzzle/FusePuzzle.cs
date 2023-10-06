using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusePuzzle : MonoBehaviour, IInteractable
{
    public string GetInteractText()
    {
        return "Place Fuse";
    }

    public void Interact()
    {
        Debug.Log("Fuse is placed");
    }
}
