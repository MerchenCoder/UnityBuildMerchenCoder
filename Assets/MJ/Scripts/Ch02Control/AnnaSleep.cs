using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnaSleep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.CheckPlayProgress("Mission2-1N") || GameManager.Instance.CheckPlayProgress("Chap2Start"))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
