using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusePuzzle1 : MonoBehaviour, IInteractable
{
    public bool isTaken = false; 
    [SerializeField] GameObject _fuses;
    [SerializeField] GameObject _fuse;


    public string GetInteractText()
    {
        if (isTaken == false)
        {
            return ("Take Fuse");
        }
        else
        {
            return ("Replace Fuse");
        }

    }

    public void Interact()
    {
        if(isTaken  == false) 
        {
            _fuse.SetActive(false);
            isTaken = true;
            Debug.Log("Fuse is Taken");
        }
        else 
        {
            _fuse.SetActive(true);
            isTaken = false;
            Debug.Log("Fuse is Replaced");
        }

    }


}
