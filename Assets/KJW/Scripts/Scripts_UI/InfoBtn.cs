using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBtn : MonoBehaviour
{
    [SerializeField] GameObject InfoPanel;

    public void InfoPanelBtnOnClick()
    {
        if (InfoPanel != null)
            InfoPanel.gameObject.SetActive(!InfoPanel.activeSelf);
        else Debug.Log("infoPanel 설정 필요");
    }
}
