using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSetActive2 : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CheckPlayProgress("Mission2-10Y") || GameManager.Instance.CheckPlayProgress("Mission2-11N")
            || GameManager.Instance.CheckPlayProgress("Mission2-11Y") || GameManager.Instance.CheckPlayProgress("Mission2-12N")
            || GameManager.Instance.CheckPlayProgress("Mission2-12Y") || GameManager.Instance.CheckPlayProgress("Mission2-13N"))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
