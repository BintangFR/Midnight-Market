using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnvirontmentController : MonoBehaviour,IInteractable
{
    public enum EnvirontmentType{
        ShelfSponsoredFood,
        ShelfDrink,
        ShelfSnack,
        Electrical,
        Telephone,
        Fan,
        Taskboard,
        CCTV
    }

    public EnvirontmentType environtmentType;
    public ItemController[] itemsList;
    public UnityEvent unityEvent;

    private bool fanInteracted = false;

    public string GetInteractText()
    {
        if (environtmentType == EnvirontmentType.Fan)
        {
            return "Turn off fan";
        }

        if (environtmentType == EnvirontmentType.Taskboard)
        {
            return "Look at taskboard";
        }

        if (environtmentType != EnvirontmentType.Telephone)
        {
            
            foreach (ItemController item in itemsList)
            {
                if (ItemManager.instance.items.Contains(item))
                {
                    return "Place " + item.name;
                }

            }
        }
    //     else if (ItemManager.instance.items.Contains(fuseOffice))
    //     {
    //         return "Place " + fuseOffice.name; 
    //     }
    //     else if (ItemManager.instance.items.Contains(boltCutter))
    //     {
    //         return "Cut chain with " + boltCutter.name; 
    //     }
    //     else{
    //         return "Look For Fuse";
    //     }

        if (environtmentType == EnvirontmentType.Telephone)
        {
            return "Call 911";
        }
        else if (environtmentType == EnvirontmentType.CCTV)
        {
            return "Watch CCTV";
        }
        return "";

    }

    public bool HasInteractedWithFan()
    {
        return fanInteracted;
    }


    public void Interact()
    {
        if (environtmentType != EnvirontmentType.Telephone) {

            foreach (ItemController item in itemsList)
            {
                if (ItemManager.instance.items.Contains(item))
                {
                    item.isPlaced = true;
                    ItemManager.instance.items.Remove(item);
                }
            }
        }
        if (environtmentType == EnvirontmentType.Telephone)
        {
            Debug.Log("Telephone Mati");
            unityEvent.Invoke();
        }

        else if (environtmentType == EnvirontmentType.Fan)
        {
            fanInteracted = true;
            Debug.Log("Fan Mati");
            unityEvent.Invoke();
        }

        else if (environtmentType == EnvirontmentType.Taskboard)
        {
            Debug.Log("Taskboard dilihat");
            unityEvent.Invoke();
        }

        else if (environtmentType == EnvirontmentType.CCTV)
        {
            Debug.Log("CCTV sudah dicek");
            unityEvent.Invoke();
        }
    }

    private void Update() {
        foreach (ItemController item in itemsList)
        {
            //bool result = Tiles.All(tile => tile.GetComponent<Stats>().IsEmpty == false);
            bool allResult = itemsList.All(item => item.isPlaced);
            if (allResult)
                {
                    gameObject.layer = default;
                    unityEvent.Invoke();
                }
        }   
    }

}
