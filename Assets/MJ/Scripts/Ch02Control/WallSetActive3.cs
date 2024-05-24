using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSetActive3 : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CheckPlayProgress("Mission2-10N"))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}