using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour, IInteractable
{
    public GameObject npcDialogue;
    private Animator npcAnimator;
    
    void Start()
    {
        Transform childTransform = transform.Find("npc idle new");

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
        if (!npcDialogue.activeSelf)
        {
            npcDialogue.SetActive(true);

            if (npcDialogue.activeSelf && npcAnimator != null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    transform.LookAt(player.transform.position);
                    transform.eulerAngles = new Vector3(-180f, transform.eulerAngles.y, -180f);
                }
                npcAnimator.SetTrigger("Talking");
            }
        }
    }

    public void ChangeDialogue(Dialogue dialogue)
    {
        npcDialogue = dialogue.gameObject;
    }
}
