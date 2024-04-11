using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBtn : MonoBehaviour
{
    GameObject InfoPanel;

    private void Start()
    {
        InfoPanel = transform.GetChild(1).gameObject;
    }

    public void InfoPanelBtnOnClick()
    {
        InfoPanel.gameObject.SetActive(!InfoPanel.activeSelf);
    }
}
