using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    // mySQL
    public string item_id;
    public string item_name;
    public string price;
    public int item_type;

    // Unity
    public Sprite item_Image;
}
