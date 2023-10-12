using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    //[SerializeField] private ItemController[] items;
    public List<ItemController> items = new List<ItemController>();
    // Start is called before the first frame update

    public static ItemManager instance;


    private void Awake() {
        instance = this;
    }


    public void AddItem(ItemController item){
        items.Add(item);
        
    }
}
