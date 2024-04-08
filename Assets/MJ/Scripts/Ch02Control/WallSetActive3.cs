using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSetActive3 : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CheckPlayProgress("Mission2-13Y") || GameManager.Instance.CheckPlayProgress("Mission2-14N"))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}