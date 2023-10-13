using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemController : MonoBehaviour,IInteractable
{
    public String name;
    public String Description;
    public UnityEvent unityEvent;
    
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
        unityEvent.Invoke();

        if (name == "Office Fuse")
        {
            Debug.Log("Keambil fuse");
            AudioManager.Instance.PlaySFX("Fuse-Picked", transform.position);
        }
        else if(name == "Bolt Cutter")
        {
            AudioManager.Instance.PlaySFX("BoltCutter-Picked", transform.position);
        }
        else if (name == "Food")
        {
            AudioManager.Instance.PlaySFX("Box-Picked", transform.position);
        }
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
