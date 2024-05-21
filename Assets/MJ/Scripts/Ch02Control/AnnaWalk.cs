using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnaWalk : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (GameManager.Instance.CheckPlayProgress("Mission2-1Y"))
        {
            gameObject.transform.localPosition = new Vector3(67.05f, -1.63f, 0);
        }
    }
}