using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunctionMaker : MonoBehaviour
{
    private GameObject Panel2;
    private FunctionManager functionManager;
    private int type = 0;
    public int Type {
        get {return type;}
        set{type = value;}
    }

    private void Start() {
        functionManager = GameObject.Find("FunctionManager").GetComponent<FunctionManager>();
        GetComponent<Button>().onClick.AddListener(SetFuncType);
        Panel2 = GameObject.Find("Canvas_FuncSetting").transform.GetChild(1).gameObject;
    }

    public void SetFuncType() {
        functionManager.ResetFuncSetting();
        Debug.Log(type);
        switch(type){
            case 1: //매개변수 없음, 반환값 없음
                functionManager.hasPara =false;                functionManager.hasReturn = false;
                break;
            case 2: //매개변수 없음, 반환값 있음
                functionManager.hasPara =false;                functionManager.hasReturn = false;
                functionManager.hasReturn = true;
                break;
            case 3: //매개변수 있음, 반환값 없음
                functionManager.hasPara =true;                functionManager.hasReturn = false;
                functionManager.hasReturn = false;
                break;
            case 4: //매개변수 있음, 반환값 있음
                functionManager.hasPara =true;                functionManager.hasReturn = false;
                functionManager.hasReturn = true;
                break;
        }
        Debug.Log(functionManager.hasPara.ToString() + functionManager.hasReturn.ToString());
        Panel2.SetActive(true);
    }
}
