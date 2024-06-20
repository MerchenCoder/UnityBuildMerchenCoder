using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTypeControl : MonoBehaviour
{
    public void SetOffChildren(GameObject _gameObject)
    {
        
        for(int i=0; i<transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name == _gameObject.name) _gameObject.SetActive(true);
            else transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
