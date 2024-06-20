using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class ItemSetButton : MonoBehaviour
{
    private ItemControl itemControl;
    public SaveItem saveItem;

    int type;
    int index;

    private void Awake()
    {
        if (itemControl == null)
        {
            itemControl = transform.parent.GetComponent<ItemControl>();
            type = itemControl._thisItem.item.item_type;
            index = int.Parse(itemControl._thisItem.item.item_id) % 100;
        }
    }

    private void Start()
    {
        if (itemControl._thisItem.isBought)
        {
            gameObject.SetActive(true);
        }
    }

    public void SetButton()
    {
        itemControl._thisItem.ItemSet();
        saveItem.SaveItemDataSet(type, index);
    }

}
