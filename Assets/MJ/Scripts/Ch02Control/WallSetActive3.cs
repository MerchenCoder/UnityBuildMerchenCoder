using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSetActive3 : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (DataManager.Instance.gameStateData.ch2MissionClear[9])
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}