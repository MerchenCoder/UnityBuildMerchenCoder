using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectParaNodePanel : MonoBehaviour
{
    private GameObject radioGroup;
    private ParaNodeBtn paraNodeBtn;
    private GameObject option1;
    private GameObject option2;

    public Sprite[] TypeImage;

    private void Start()
    {


    }

    private void OnDisable()
    {

        radioGroup.GetComponent<ToggleGroup2Manager>().ResetToggleSelection();
    }

    private void OnEnable()
    {
        radioGroup = transform.GetChild(0).GetChild(2).gameObject;
        paraNodeBtn = transform.parent.GetComponentInChildren<ParaNodeBtn>();
        // GetChild(0).Find("Nodes").
        option1 = radioGroup.transform.GetChild(0).gameObject;
        option2 = radioGroup.transform.GetChild(1).gameObject;
        SetInfo(option1, 1);
        SetInfo(option2, 2);

    }


    void SetInfo(GameObject option, int num)
    {
        if (num == 1)
        {
            option.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = paraNodeBtn.Para1Name;
            option.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = TypeImage[paraNodeBtn.Para1Type];
            option.transform.GetChild(2).GetChild(2).GetComponent<Text>().text = paraNodeBtn.TypeToText(paraNodeBtn.Para1Type);

        }
        else
        {
            option.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = paraNodeBtn.Para2Name;
            option.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = TypeImage[paraNodeBtn.Para2Type];
            option.transform.GetChild(2).GetChild(2).GetComponent<Text>().text = paraNodeBtn.TypeToText(paraNodeBtn.Para2Type);
        }
    }
}
