using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour,IInteractable
{
    public String name;
    public String Description;
    
    [SerializeField] private bool isObtained;
    public bool isPlaced;

    public string GetInteractText()
    {
        return "Pick up "+ name;
    }

    public void Interact()
    {
        isObtained = true;
        ItemManager.instance.AddItem(this);
        gameObject.active = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
