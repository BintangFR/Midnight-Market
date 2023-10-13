using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltCutterPuzzle : MonoBehaviour, IInteractable
{
    public string GetInteractText()
    {
        return "Cut the chain";
    }

    public ObjectiveTrigger objectiveTrigger;

    public void Interact()
    {
        Debug.Log("The door has opened");

        
        if (objectiveTrigger != null)
        {
            objectiveTrigger.startObjective.Invoke();
        }

        AudioManager.Instance.PlaySFX("Chain", transform.position);
    }
}