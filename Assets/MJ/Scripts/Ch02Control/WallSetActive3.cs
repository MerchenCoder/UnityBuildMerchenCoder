using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSetActive3 : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CheckPlayProgress("Mission2-10Y")
            || GameManager.Instance.CheckPlayProgress("Mission2-11N")
            || GameManager.Instance.CheckPlayProgress("Mission2-11Y")
            || GameManager.Instance.CheckPlayProgress("Mission2-12N")
            || GameManager.Instance.CheckPlayProgress("Mission2-12Y")
            || GameManager.Instance.CheckPlayProgress("Mission2-13N")
            || GameManager.Instance.CheckPlayProgress("Mission2-13Y")
            || GameManager.Instance.CheckPlayProgress("Mission2-14N")
            || GameManager.Instance.CheckPlayProgress("Mission2-14Y")
            || GameManager.Instance.CheckPlayProgress("Mission2-15N")
            || GameManager.Instance.CheckPlayProgress("Mission2-15Y")
            || GameManager.Instance.CheckPlayProgress("Chap2Ending"))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}