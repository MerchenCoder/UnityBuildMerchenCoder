using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    private bool hasExecuted = false;

    void Update()
    {
        if (!hasExecuted && DataManager.Instance.gameStateData.ch2MissionClear[4] && !GameManager.Instance.CheckPlayProgress("Mission2-5Y"))
        {
            this.transform.localPosition = new Vector3(26.8f, -1.6f, 0f);
            hasExecuted = true; // Set the flag to true after execution
        }
    }
}