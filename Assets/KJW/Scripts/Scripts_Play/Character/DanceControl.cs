using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceControl : MonoBehaviour
{
    void Start()
    {
        if(GameManager.Instance.CheckPlayProgress("Chap1Ending"))
        {
            GetComponent<Animator>().enabled = false;   
        }
    }

    public void ReDance()
    {
        GetComponent<Animator>().enabled = true;
    }

}
