using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayProgressControl : MonoBehaviour
{
    [SerializeField] string activePlayPoint;

    private void Awake()
    {
        gameObject.SetActive(true);
    }

    void Start()
    {
        if (GameManager.Instance.CheckPlayProgress(activePlayPoint))
        {
            gameObject.SetActive(true);
        }
        else gameObject.SetActive(false);
    }

}
