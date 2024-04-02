using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningPanel : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DisableDalay", 1f);
    }

    public void DisableDalay()
    {
        gameObject.SetActive(false);
    }
}
