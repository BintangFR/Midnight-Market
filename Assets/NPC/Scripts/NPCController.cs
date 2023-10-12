using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour,IInteractable
{

    public GameObject npcDialogue;

    
    public string GetInteractText()
    {
        return "Talk To Fauzan";
    }

    public void Interact()
    {        
        npcDialogue.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDialogue(Dialogue dialogue){
        npcDialogue = dialogue.gameObject;
    }
}
