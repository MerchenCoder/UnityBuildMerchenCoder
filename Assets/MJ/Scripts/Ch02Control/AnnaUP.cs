using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnaUP : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance.CheckPlayProgress("Chap2Start"))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}