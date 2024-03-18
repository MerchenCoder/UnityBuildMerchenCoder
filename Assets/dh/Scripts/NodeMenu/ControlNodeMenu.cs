using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlNodeMenu : MonoBehaviour
{
    GameObject PanelTabBar;
    GameObject StartEndPanel;
    GameObject ActionPanel;
    GameObject FuncPanel;


    // Start is called before the first frame update
    void Start()
    {
        PanelTabBar = transform.GetChild(9).gameObject;
        if (GameManager.Instance.missionData.hasNodeLimit)
        {
            ControlNodeTabBar(GameManager.Instance.missionData.nodeOpenIndex);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ControlNodeTabBar(int index)
    {
        Debug.Log(index.ToString() + "까지만 오픈");
        Transform PanelTabBarContent = PanelTabBar.transform.GetChild(0);
        for (int i = index + 1; i < PanelTabBarContent.childCount; i++)
        {
            PanelTabBarContent.GetChild(i).gameObject.SetActive(false);
        }

    }
}
