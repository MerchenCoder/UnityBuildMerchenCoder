using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lily : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance.CheckPlayProgress("FindLily") ||
            GameManager.Instance.CheckPlayProgress("HelpLilyN") ||
            GameManager.Instance.CheckPlayProgress("HelpLilyY"))
        {
            gameObject.SetActive(true);
        }
        else gameObject.SetActive(false);
    }


}
