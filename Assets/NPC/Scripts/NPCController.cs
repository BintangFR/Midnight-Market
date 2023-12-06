using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour,IInteractable
{

    public GameObject npcDialogue;

    public string GetInteractText()
    {
        return "Talk To Adam";
    }

    public void Interact()
    {        
        npcDialogue.SetActive(true);       
    }

    public void ChangeDialogue(Dialogue dialogue){
        npcDialogue = dialogue.gameObject;
    }
}
