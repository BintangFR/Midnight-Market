using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoltCutterPuzzle : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemController boltCutter;
    [SerializeField] private UnityEvent unityEvent;
    public string GetInteractText()
    {
        if (ItemManager.instance.items.Contains(boltCutter))
        {
            
            return "Cut the chain";
        }
        return "Look for Bolt Cutter";
    }


    public void Interact()
    {
        Debug.Log("The door has opened");

        
        if (ItemManager.instance.items.Contains(boltCutter))
        {
            unityEvent.Invoke();
            gameObject.SetActive(false);
        }

        AudioManager.Instance.PlaySFX("Chain", transform.position);
    }
}