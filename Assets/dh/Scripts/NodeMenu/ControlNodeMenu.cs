using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlNodeMenu : MonoBehaviour
{
    GameObject PanelTabBar;


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
            PanelTabBarContent.GetChild(i).gameObject.SetActive(isTabOpenList[i]);
        }

    }

    // public void AddNodeBtn(GameObject nodeBtn, int tabIdx)
    // {
    //     nodeBtn.transform.SetParent(transform.GetChild(tabIdx).GetChild(0), false);
    // }
}
