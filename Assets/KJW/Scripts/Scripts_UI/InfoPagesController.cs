using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPagesController : MonoBehaviour
{
    private void SetActiveFalseAllPages()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
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
        SetActiveFalseAllPages();
        string pageName = "Page_" + tabName;
        for (int i = 1; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name == pageName)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                break;
            }
        }
    }

    private void Start()
    {
        InfoTabButtonOnClick("InfoTabBtn_Node");
    }

}
