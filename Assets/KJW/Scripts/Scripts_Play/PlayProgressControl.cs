using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayProgressControl : MonoBehaviour
{
    public string activePlayPoint;

    void Start()
    {
        if (GameManager.Instance.CheckPlayProgress(activePlayPoint))
        {
            gameObject.SetActive(true);
        }
        else gameObject.SetActive(false);
    }

}
