using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnvirontmentController : MonoBehaviour,IInteractable
{
    public enum EnvirontmentType{
        ShelfSponsoredFood,
        ShelfDrink,
        ShelfSnack,
        Electrical,
    }

    public EnvirontmentType environtmentType;
    public ItemController[] itemsList;

    public string GetInteractText()
    {
        foreach (ItemController item in itemsList)
        {
            if (ItemManager.instance.items.Contains(item))
            {
                return "Place " + item.name;
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
        return "";
        
     }

    public void Interact()
    {
        foreach (ItemController item in itemsList)
        {
            if (ItemManager.instance.items.Contains(item))
            {
                item.isPlaced = true;
                ItemManager.instance.items.Remove(item); 
            }
            

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
                }
        }   
    }

}
