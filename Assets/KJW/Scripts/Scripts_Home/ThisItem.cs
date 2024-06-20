using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisItem : MonoBehaviour
{
    public Item item;
    public bool isBought;
    //List<string> items = new List<string>();

    void Start()
    {
        //isBought = false;
        //LoadItemList();
    }

    //void LoadItemList()
    //{
    //    items = transform.parent.parent.GetComponent<SaveItem>().itemData.boughtItems;

    //    for(int i=0; i<items.Count; i++)
    //    {
    //        if(item.item_name == items[i])
    //        {
    //            isBought = true;
    //        }
    //    }
    //}

    public void ItemSet()
    {
        transform.parent.GetComponent<ItemTypeControl>().SetOffChildren(gameObject);
    }
}
