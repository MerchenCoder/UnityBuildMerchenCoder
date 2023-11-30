using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOnOff : MonoBehaviour
{
    public GameObject Panel;

    private void Start()
    {
        Panel.SetActive(false);
    }

    public void PanelVisible()
    {
        Debug.Log("클릭");
        Panel.SetActive(true);
    }

}

