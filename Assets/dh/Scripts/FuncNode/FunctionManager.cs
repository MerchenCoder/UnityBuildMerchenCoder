using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionManager : MonoBehaviour
{
    public bool hasPara = false;
    public bool hasPara1 = false;
    public bool hasPara2 = false;
    public bool hasReturn = false;



    private string funcName = null;
    public string FunName{
        get{
            return funcName;
        }
        set {
            funcName = value;
        }
    }

    private string para1Name = null;
    private string para2Name = null;




    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ResetFuncSetting() {
        hasPara = false;
        hasPara1 = false;
        hasPara2 = false;
        hasReturn = false;
        funcName = null;
    }
}
