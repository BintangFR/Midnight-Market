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
    [SerializeField] private  UnityEvent unityEvent;
    [SerializeField] private  UnityEvent StorageFuseEvent;
    [SerializeField] private  UnityEvent OfficeFuseEvent;
    [SerializeField] private GameObject storageFuse;
    [SerializeField] private GameObject officeFuse;

    private bool hasInteracted;

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

        if (environtmentType != EnvirontmentType.Electrical)
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

        if (environtmentType == EnvirontmentType.ShelfSponsoredFood) {
            foreach (ItemController item in itemsList)
            {
                if (ItemManager.instance.items.Contains(item))
                {
                    item.isPlaced = true;
                    item.isObtained = false;

                    ItemManager.instance.items.Remove(item);
                    unityEvent.Invoke();
                }
            }

        }


        if (environtmentType == EnvirontmentType.Electrical) {
            foreach (ItemController item in itemsList)
            {
                if (ItemManager.instance.items.Contains(item))
                {
                    item.isPlaced = true;
                    item.isObtained = false;
                    ItemManager.instance.items.Remove(item);
                }
                
                
            }
        }


        if (environtmentType == EnvirontmentType.Telephone && !hasInteracted)
        {
            StartCoroutine(Calling());

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
        else if (environtmentType == EnvirontmentType.ShelfSponsoredFood)
        {
            Debug.Log("Makanan ditaruh");
            AudioManager.Instance.PlaySFX("ItemPlaced-Shelf", transform.position);
            AudioManager.Instance.PlaySFX("Boom", transform.position);
        }
    }

    private IEnumerator Calling(){
        AudioManager audioManager = AudioManager.Instance;
        hasInteracted = true;
        if (audioManager != null)
        {
            audioManager.PlaySFX("Phone", transform.position);
        }
        Debug.Log("Telephone Mati");
        yield return new WaitForSeconds(6.0f);
        unityEvent.Invoke();
        yield return new WaitForEndOfFrame();
        gameObject.layer = 0;
    }

    private void Update() {

        if (environtmentType == EnvirontmentType.Electrical)
        {
            if (itemsList[0].isPlaced)
            {
                itemsList[0].isPlaced = false;
                StorageFuseEvent.Invoke();
                storageFuse.SetActive(true);
                
            }
            if (itemsList[1].isPlaced)
            {
                itemsList[1].isPlaced = false;
                OfficeFuseEvent.Invoke();
                officeFuse.SetActive(true);
            }
        }
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
