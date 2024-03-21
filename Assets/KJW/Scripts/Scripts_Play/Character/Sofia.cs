using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sofia : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance.CheckPlayProgress("Chap1Ending"))
        {
            this.transform.localPosition = new Vector3(23.5f, -1.09f, 0);
        }
    }

}
