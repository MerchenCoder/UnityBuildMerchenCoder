using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorganActive : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (GameManager.Instance.CheckPlayProgress("Mission2-9Y"))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
