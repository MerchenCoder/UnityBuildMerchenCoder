using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FunctionForm : MonoBehaviour
{
    private Transform form;
    private FunctionManager functionManager;

    private void OnEnable() 
    {
        form = transform.GetChild(0).GetChild(3);
        functionManager = GameObject.Find("FunctionManager").GetComponent<FunctionManager>();  
        //form reset
        ResetForms();

        if(functionManager.Type>=3){ //hasPara
            form.GetChild(1).gameObject.SetActive(true);
            form.GetChild(2).gameObject.SetActive(true);       
        }
        else {
            form.GetChild(1).gameObject.SetActive(false);
            form.GetChild(2).gameObject.SetActive(false);    
        }
        if(functionManager.Type%2==0){
            form.GetChild(3).gameObject.SetActive(true);
        }
        else {
            form.GetChild(3).gameObject.SetActive(false);
        }
    }

    public void ResetForms() {
        for(int i=0;i<4;i++){
            //자식들 모두 껐다가 켜주면서 자손들까지 reset도 같이!
            form.GetChild(i).gameObject.SetActive(false);
            form.GetChild(i).gameObject.SetActive(true);
        }
    }
}
