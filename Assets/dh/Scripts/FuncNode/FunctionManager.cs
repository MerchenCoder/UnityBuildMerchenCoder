using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionManager : MonoBehaviour
{
    public bool hasPara = false;
    public bool hasPara1 = false;
    public bool hasPara2 = false;
    public bool hasReturn = false;


    //타입 : 0=int, 1=bool, 2=string, -1=초기화, 없음 (드롭다운 option index랑 동일값으로 한다)
    public int para1Type = -1;
    public int para2Type = -1;
    public int returnType = -1;




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
    public string Para1Name{
        get{
            return para1Name;
        }
        set {
            para1Name = value;
        }
    }
    public string Para2Name{
        get{
            return para2Name;
        }
        set {
            para2Name = value;
        }
        
    }


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


        para1Type = -1;
        para2Type = -1;
        returnType = -1;
    }
}
