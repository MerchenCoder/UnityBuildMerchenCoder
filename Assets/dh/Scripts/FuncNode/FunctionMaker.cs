using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunctionMaker : MonoBehaviour
{
    private GameObject Panel2;
    private FunctionManager functionManager;
   public int selectType = 0;

    private void Start() {
        functionManager = GameObject.Find("FunctionManager").GetComponent<FunctionManager>();
        GetComponent<Button>().onClick.AddListener(SetFuncType);
        Panel2 = GameObject.Find("Canvas_FuncSetting").transform.GetChild(1).gameObject;
    }

    public void SetFuncType() {
        functionManager.ResetFuncSetting();
        functionManager.Type = selectType;
        Panel2.SetActive(true);
    }
}
