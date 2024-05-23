using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InfoPagesController : MonoBehaviour
{
    ToggleGroup toggleGroup;

    private void Awake()
    {
        toggleGroup = GetComponent<ToggleGroup>();
    }

    private void SetActiveFalseAllPages()
    {
        toggleGroup.SetAllTogglesOff();
    }

    public void InfoTabButtonOnClick(string objectName)
    {
        string[] pageName = objectName.Split('_');
        SetActiveTrueOnePage(pageName[1]);
    }

    /// <summary>
    /// 노드 튜토리얼 대화에서 호출 시 사용
    /// </summary>
    /// <param name="tabName"></param>
    public void SetActiveTrueOnePage(string tabName)
    {
        if (toggleGroup == null)
        {
            toggleGroup = GetComponent<ToggleGroup>();
        }
        string pageName = "InfoTabToggle_" + tabName;
        string currentInfoPange;
        Debug.Log(toggleGroup);
        Debug.Log(toggleGroup.ActiveToggles().FirstOrDefault());
        Debug.Log(toggleGroup.ActiveToggles());
        if (toggleGroup.ActiveToggles().FirstOrDefault() != null)
        {
            currentInfoPange = toggleGroup.ActiveToggles().FirstOrDefault().name;
            if (pageName.Equals(currentInfoPange))
            {
                return;
            }

        }
        SetActiveFalseAllPages();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name == pageName)
            {
                // transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).GetComponent<Toggle>().isOn = true;
                break;
            }
        }
    }
}
