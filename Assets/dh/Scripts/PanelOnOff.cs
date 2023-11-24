using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOnOff : MonoBehaviour
{
    public GameObject Panel;

    public void PanelVisible()
    {
        Panel.SetActive(true);
    }

}

