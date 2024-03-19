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
            ControlNodeTabBar(GameManager.Instance.missionData.isTabOpenList);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ControlNodeTabBar(bool[] isTabOpenList)
    {
        Transform PanelTabBarContent = PanelTabBar.transform.GetChild(0);
        for (int i = 0; i < isTabOpenList.Length; i++)
        {
            Debug.Log("인덱스" + i.ToString() + "는" + isTabOpenList[i].ToString());
            PanelTabBarContent.GetChild(i).gameObject.SetActive(isTabOpenList[i]);
        }

    }
}
