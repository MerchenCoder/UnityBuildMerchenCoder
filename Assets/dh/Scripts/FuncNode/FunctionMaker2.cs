using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FunctionMaker2 : MonoBehaviour
{
    private GameObject funNameRow;
    private GameObject para1Row;
    private GameObject para2Row;
    private GameObject returnRow;
    public FunctionManager functionManager;

    private TMP_InputField funName;
    private TMP_InputField para1Name;
    private TMP_InputField para2Name;
    

    private Button button;
    
    private void OnEnable() {
        button = this.GetComponent<Button>();

        Transform form = transform.parent.GetChild(3).transform;
        funNameRow = form.GetChild(0).gameObject;
        if(functionManager.hasPara){
            para1Row = form.GetChild(1).gameObject;
            para2Row = form.GetChild(2).gameObject;
        }
        if(functionManager.hasReturn){
            returnRow = form.GetChild(3).gameObject;
        }
    }

    private void Update() {
        // bool allFieldsHasValue = !string.IsNullOrEmpty()
        


        // button.interactable = allFieldsHasValue;
    }



    public void FuncSetting() {
        //함수 이름
        //함수 매개변수 타입, 이름
        //함수 반환값 타입

        functionManager.FunName = funNameRow.transform.GetChild(1).GetComponent<TMP_InputField>().text;
        if(functionManager.hasPara){
            if(para1Row.activeSelf){
                functionManager.hasPara1 = true;
                functionManager.para1Type = para1Row.transform.GetChild(1).GetComponent<TMP_Dropdown>().value;
                functionManager.Para1Name = para1Row.transform.GetChild(2).GetComponent<TMP_InputField>().text;
            }
            if(para2Row.activeSelf){
                functionManager.hasPara2 = true;
                functionManager.para2Type = para2Row.transform.GetChild(1).GetComponent<TMP_Dropdown>().value;
                functionManager.Para2Name = para2Row.transform.GetChild(2).GetComponent<TMP_InputField>().text;
            }

        }
        if(functionManager.hasReturn){
            functionManager.returnType = returnRow.transform.GetChild(1).GetComponent<TMP_Dropdown>().value;
        }



    }
}
