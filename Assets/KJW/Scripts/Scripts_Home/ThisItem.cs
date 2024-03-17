using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisItem : MonoBehaviour
{
    public Item item;
    public bool isBought; 

    void Start()
    {
        // 추후 구매 정보 세이브 불러오기 추가
    }

    public void ItemSet()
    {
        int size = transform.parent.childCount;
        // Off others
        for(int i = 0; i < size; i++)
        {
            if(transform.parent.GetChild(i).gameObject != gameObject)
            {
                transform.parent.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
