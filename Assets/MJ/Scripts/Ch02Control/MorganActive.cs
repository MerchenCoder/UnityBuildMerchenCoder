using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorganActive : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (GameManager.Instance.CheckPlayProgress("Mission2-7Y") || GameManager.Instance.CheckPlayProgress("Mission2-8N")
            || GameManager.Instance.CheckPlayProgress("Mission2-8Y") || GameManager.Instance.CheckPlayProgress("Mission2-9N"))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
