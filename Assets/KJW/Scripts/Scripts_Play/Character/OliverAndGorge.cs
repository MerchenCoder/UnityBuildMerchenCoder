using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OliverAndGorge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.CheckPlayProgress("Mission1-12Y") || GameManager.Instance.CheckPlayProgress("Chap1Ending"))
        {
            gameObject.transform.localPosition = new Vector3(0.4f, -1.02f, 0);
        }
        else gameObject.transform.localPosition = new Vector3(14.48f, -1.02f, 0);
    }

}
