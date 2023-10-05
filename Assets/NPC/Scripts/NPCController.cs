using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour,IInteractable
{

    public GameObject npcDialogue;
    public NavMeshAgent agent;
    [SerializeField] private Animator animator;
    
    public string GetInteractText()
    {
        return "Talk To Fauzan";
    }

    public void Interact()
    {        
        npcDialogue.SetActive(true);
    }

    public void GoToDestinantion(Transform Destination){
        animator.SetFloat("Speed",agent.speed);

        agent.SetDestination(Destination.position);
    }

    public void ChangeDialogue(Dialogue dialogue){
        npcDialogue = dialogue.gameObject;
    }
}
