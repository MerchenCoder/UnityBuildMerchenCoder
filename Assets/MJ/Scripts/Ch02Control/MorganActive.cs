using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorganActive : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (DataManager.Instance.gameStateData.ch2MissionClear[8])
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
