using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSetActive : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CheckPlayProgress("Mission2-1N") || GameManager.Instance.CheckPlayProgress("Mission2-1Y")
            || GameManager.Instance.CheckPlayProgress("Mission2-2N") || GameManager.Instance.CheckPlayProgress("Mission2-2Y")
            || GameManager.Instance.CheckPlayProgress("Mission2-3N") || GameManager.Instance.CheckPlayProgress("Mission2-3Y")
            || GameManager.Instance.CheckPlayProgress("Mission2-4N") || GameManager.Instance.CheckPlayProgress("Chap2Start"))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
