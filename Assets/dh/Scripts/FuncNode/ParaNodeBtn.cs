using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParaNodeBtn : MonoBehaviour
{

    public GameObject paraNodePrefab;
    private Transform spawnPoint;

    private GameObject selectParaNodePanel;


    //paraNode 정보
    private int para1Type = -1;
    private string para1Name = null;

    public int Para1Type
    {
        get { return para1Type; }
        set { para1Type = value; }
    }
    public string Para1Name
    {
        get { return para1Name; }
        set { para1Name = value; }
    }
    private int para2Type = -1;
    private string para2Name = null;

    public int Para2Type
    {
        get { return para2Type; }
        set { para2Type = value; }
    }
    public string Para2Name
    {
        get { return para2Name; }
        set { para2Name = value; }
    }

    public string TypeToText(int type)
    {
        if (type == 0)
        {
            return "숫자";
        }
        else if (type == 1)
        {
            return "불린";
        }
        else
        {
            return "문자";
        }
    }

    Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(selectParaType);

        spawnPoint = transform.GetComponentInParent<Canvas>().transform.GetChild(0).GetChild(0).transform;

        selectParaNodePanel = this.transform.GetComponentInParent<Canvas>().transform.GetChild(1).gameObject;
        if (selectParaNodePanel.activeSelf)
        {
            selectParaNodePanel.SetActive(false);
        }
    }


    public void selectParaType()
    {
        if (para1Type >= 0 && para2Type >= 0)
        {
            //매개변수 2개인 경우
            selectParaNodePanel.SetActive(true);
        }
        else
        {
            if (para1Type >= 0)
            {
                MakeInstance(1);
            }
            else
            {
                MakeInstance(2);
            }
        }

    }

    public void MakeInstance(int paraNum)
    {
        GameObject paraNodeInstance = Instantiate(paraNodePrefab);
        if (paraNum == 1)
        {
            paraNodeInstance.GetComponent<ParaNode>().SetParaNode(para1Type, para1Name, paraNum);


        }
        else
        {
            paraNodeInstance.GetComponent<ParaNode>().SetParaNode(para2Type, para2Name, paraNum);
        }
        paraNodeInstance.GetComponent<NodeNameManager>().NodeName = "ParaNode";
        paraNodeInstance.transform.SetParent(spawnPoint, false);
        paraNodeInstance.transform.localPosition = Vector3.zero;
    }


    public void resetParaNodeBtn()
    {
        para1Name = null;
        para2Name = null;
        para1Type = -1;
        para2Type = -1;
    }
}
