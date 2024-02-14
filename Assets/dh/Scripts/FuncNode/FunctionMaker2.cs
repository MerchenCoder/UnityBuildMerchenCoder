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
    private ParameterSetting para1OnOffBtn;
    private ParameterSetting para2OnOffBtn;

    private TMP_Dropdown para1Type;
    private TMP_Dropdown para2Type;
    private TMP_Dropdown returnType;


    private Button button;

    bool allFieldsHasValue;

    bool funNameCheck;
    bool paraNameCheck;


    private void OnEnable()
    {

        //변수 초기화
        FunctionMaker2Reset();


        //버튼 컴포넌트 가져오기
        button = this.GetComponent<Button>();

        //form 컴포넌트 가져오기
        Transform form = transform.parent.GetChild(3).transform;
        funNameRow = form.GetChild(0).gameObject;
        if (functionManager.Type >= 3)
        {
            para1Row = form.GetChild(1).gameObject;
            para2Row = form.GetChild(2).gameObject;

            para1OnOffBtn = para1Row.transform.GetChild(0).GetChild(0).GetComponent<ParameterSetting>();
            para2OnOffBtn = para2Row.transform.GetChild(0).GetChild(0).GetComponent<ParameterSetting>();
            para1Type = para1Row.transform.GetChild(1).GetComponent<TMP_Dropdown>();
            para2Type = para2Row.transform.GetChild(1).GetComponent<TMP_Dropdown>();
            para1Name = para1Row.transform.GetChild(2).GetComponent<TMP_InputField>();
            para2Name = para2Row.transform.GetChild(2).GetComponent<TMP_InputField>();

            para2Name.transform.GetChild(1).gameObject.SetActive(false); //error message 안보이게
            // para2Name.onValueChanged.AddListener(checkParaName);


        }
        if (functionManager.Type % 2 == 0)
        {
            returnRow = form.GetChild(3).gameObject;
            returnType = returnRow.transform.GetChild(1).GetComponent<TMP_Dropdown>();
        }

        funName = funNameRow.transform.GetChild(1).GetComponent<TMP_InputField>();

        funName.transform.GetChild(1).gameObject.SetActive(false); //error message 안보이게
        funName.onValueChanged.AddListener(checkFunName);


    }

    private void Update()
    {

        //모든 input 값을 입력했는지 check -> 모두 입력했을 때만 button active;
        switch (functionManager.Type)
        {
            case 1:
                allFieldsHasValue = FunNameCheckFields() && funNameCheck;
                break;
            case 2:
                allFieldsHasValue = FunNameCheckFields() && ReturnCheckFields() && funNameCheck;
                break;
            case 3:
                allFieldsHasValue = FunNameCheckFields() && ParaCheckFields() && funNameCheck;
                break;
            case 4:
                allFieldsHasValue = FunNameCheckFields() && ParaCheckFields() && ReturnCheckFields() && funNameCheck;
                break;
            default:
                Debug.Log("allFieldHasValue 체크 오류");
                break;
        }


        button.interactable = allFieldsHasValue;
    }


    bool ParaCheckFields()
    {
        bool result = false;
        if (para1OnOffBtn.IsOn || para2OnOffBtn.IsOn)
        {
            if (para1OnOffBtn.IsOn)
            {
                result = para1Type.value != -1 && !string.IsNullOrEmpty(para1Name.text);
                para2Name.transform.GetChild(1).gameObject.SetActive(false);
            }
            if (para2OnOffBtn.IsOn)
            {
                result = para2Type.value != -1 && !string.IsNullOrEmpty(para2Name.text);
                para2Name.transform.GetChild(1).gameObject.SetActive(false);
            }

        }
        if (para1OnOffBtn.IsOn && para2OnOffBtn.IsOn)
        {
            if (para1Name.text == para2Name.text)
            {
                if (para1Name.text != "")
                {
                    para2Name.transform.GetChild(1).gameObject.SetActive(true);
                    result = false;
                }
                else
                {
                    para2Name.transform.GetChild(1).gameObject.SetActive(false);
                }

            }
            else
            {
                para2Name.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        return result;
    }



    bool ReturnCheckFields()
    {

        return returnType.value != -1;
    }

    bool FunNameCheckFields()
    {
        return !string.IsNullOrEmpty(funName.text);
    }


    public void FuncSetting()
    {
        //함수 이름 / 함수 매개변수 타입, 이름 / 함수 반환값 타입 값 넘겨주기

        //함수 이름
        functionManager.FunName = funNameRow.transform.GetChild(1).GetComponent<TMP_InputField>().text;
        //매개변수 타입, 이름 (type 3, 4)
        if (functionManager.Type >= 3)
        {
            if (para1OnOffBtn.IsOn)
            {
                functionManager.hasPara1 = true;
                functionManager.para1Type = para1Row.transform.GetChild(1).GetComponent<TMP_Dropdown>().value;
                functionManager.Para1Name = para1Row.transform.GetChild(2).GetComponent<TMP_InputField>().text;
            }
            if (para2OnOffBtn.IsOn)
            {
                functionManager.hasPara2 = true;
                functionManager.para2Type = para2Row.transform.GetChild(1).GetComponent<TMP_Dropdown>().value;
                functionManager.Para2Name = para2Row.transform.GetChild(2).GetComponent<TMP_InputField>().text;
            }

        }
        if (functionManager.Type % 2 == 0)
        {
            functionManager.returnType = returnRow.transform.GetChild(1).GetComponent<TMP_Dropdown>().value;
        }

        functionManager.CreateFunctionMakeCanvas();
        functionManager.CreateFunctionNode();

        transform.GetComponentInParent<Canvas>().gameObject.SetActive(false);


    }



    void FunctionMaker2Reset()
    {
        funNameRow = null;
        para1Row = null;
        para2Row = null;
        returnRow = null;

        funName = null;
        para1Name = null;
        para2Name = null;
        para1OnOffBtn = null;
        para2OnOffBtn = null;
        para1Type = null;
        para2Type = null;
        returnType = null;


        allFieldsHasValue = false;
    }


    private void checkFunName(string inputText)
    {
        foreach (GameObject function in FunctionManager.Instance.myfuncCanvas)
        {
            if (function.name.Split("_")[0].ToString() == inputText)
            {
                funName.transform.GetChild(1).gameObject.SetActive(true);
                funNameCheck = false;
                return;

            }
            funName.transform.GetChild(1).gameObject.SetActive(false);
        }
        funNameCheck = true;
    }

    // private void checkParaName(string inputText)
    // {
    //     if (para1OnOffBtn.IsOn && para2OnOffBtn.IsOn)
    //     {
    //         if (inputText != null && para1Name.text != null && inputText == para1Name.text)
    //         {
    //             para2Name.transform.GetChild(1).gameObject.SetActive(true);
    //             paraNameCheck = false;
    //         }
    //         else
    //         {
    //             para2Name.transform.GetChild(1).gameObject.SetActive(false);
    //             paraNameCheck = true;
    //         }
    //     }


    // }
}
