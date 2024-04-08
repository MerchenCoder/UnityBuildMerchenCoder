using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSetActive1 : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CheckPlayProgress("Mission2-7Y") || GameManager.Instance.CheckPlayProgress("Mission2-8N")
            || GameManager.Instance.CheckPlayProgress("Mission2-8Y") || GameManager.Instance.CheckPlayProgress("Mission2-9N"))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
