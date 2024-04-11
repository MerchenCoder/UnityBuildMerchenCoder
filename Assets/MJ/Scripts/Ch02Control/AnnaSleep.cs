using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnaSleep : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (GameManager.Instance.CheckPlayProgress("Chap2Start") || GameManager.Instance.CheckPlayProgress("Mission2-1N"))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
