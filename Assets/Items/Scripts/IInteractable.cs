using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface for interactable item
public interface IInteractable 
{
    String GetInteractText();
    void Interact();
}
