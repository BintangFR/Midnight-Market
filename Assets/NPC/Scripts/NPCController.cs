using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour,IInteractable
{

    public GameObject npcDialogue;
    private Animator npcAnimator; 

    void Start()
    {
        Transform childTransform = transform.Find("NPC Idle");

        if (childTransform != null)
        {
            npcAnimator = childTransform.GetComponent<Animator>();
        }
    }

    public string GetInteractText()
    {
        return "Talk To Fauzan";
    }

    public void Interact()
    {        
        npcDialogue.SetActive(true);

        if (npcDialogue.activeSelf) 
        {
            if (npcAnimator != null)
            {
                npcAnimator.SetTrigger("Talking");
            }
        }
    }

    public void ChangeDialogue(Dialogue dialogue){
        npcDialogue = dialogue.gameObject;
    }
}
