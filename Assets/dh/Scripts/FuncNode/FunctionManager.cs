using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionManager : MonoBehaviour
{
    //외부에서는 type에 접근해서 읽고 쓸 수 있음
    private int type = 0; //초기화 0(아무것도 아닌 타입)
    //type을 바꾸면 haspara... 등 설정 자동으로 변경
    public int Type {
        get{
            return type;
        }
        set{
            if(value==0){
                hasPara = false;
                hasPara1 = false;
                hasPara2 = false;
                hasReturn = false;
            }
            if(value>=3){
                hasPara = true;
            }
            else{
                hasPara = false;
            }
            if(value%2==0){
                hasReturn=true;
            }
            else{
                hasReturn=false;
            }
            type=value;
        }
    }
    bool hasPara = false;
    bool hasReturn = false;

    // public bool HasPara {
    //     get{
    //         return hasPara;
    //     }
    // }

    // public bool HasReturn {
    //     get{
    //         return hasReturn;
    //     }
    // }


    public bool hasPara1 = false;
    public bool hasPara2 = false;
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
        Type = 0;
        funcName = null;
        para1Type = -1;
        para2Type = -1;
        returnType = -1;
    }
}
