using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionManager : MonoBehaviour
{
    //외부에서는 type에 접근해서 읽고 쓸 수 있음
    private int type = 0; //초기화 0(아무것도 아닌 타입)
    //type을 바꾸면 haspara... 등 설정 자동으로 변경
    public int Type
    {
        get
        {
            return type;
        }
        set
        {
            if (value == 0)
            {
                hasPara = false;
                hasPara1 = false;
                hasPara2 = false;
                hasReturn = false;
            }
            if (value >= 3)
            {
                hasPara = true;
            }
            else
            {
                hasPara = false;
            }
            if (value % 2 == 0)
            {
                hasReturn = true;
            }
            else
            {
                hasReturn = false;
            }
            type = value;
        }
    }
    bool hasPara = false;
    bool hasReturn = false;

    public bool hasPara1 = false;
    public bool hasPara2 = false;
    //타입 : 0=int, 1=bool, 2=string, -1=초기화, 없음 (드롭다운 option index랑 동일값으로 한다)
    public int para1Type = -1;
    public int para2Type = -1;
    public int returnType = -1;


    private string funcName = null;
    public string FunName
    {
        get
        {
            return funcName;
        }
        set
        {
            funcName = value;
        }
    }

    private string para1Name = null;

    private string para2Name = null;
    public string Para1Name
    {
        get
        {
            return para1Name;
        }
        set
        {
            para1Name = value;
        }
    }
    public string Para2Name
    {
        get
        {
            return para2Name;
        }
        set
        {
            para2Name = value;
        }

    }


    //for create canvas instance
    public GameObject canvasFuncMakePrefab;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void ResetFuncSetting()
    {
        Type = 0;
        funcName = null;
        para1Type = -1;
        para2Type = -1;
        returnType = -1;
    }




    public void CreateFunctionMakeCanvas()
    {
        Debug.Log("generate function make panel 호출");
        //캔버스 프리팹 생성
        GameObject canvasPrefabInstance = Instantiate(canvasFuncMakePrefab);

        //캔버스 렌더링 모드 설정(UI 카메라로 변경)
        Canvas canvas = canvasPrefabInstance.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;

            Camera[] cameras = FindObjectsOfType<Camera>();
            foreach (Camera camera in cameras)
            {
                if (camera.name == "UI_Camera") // 여기서 "MyCameraName"은 원하는 카메라 이름으로 바꿔야 합니다.
                {
                    canvas.worldCamera = camera;
                    break; // 원하는 카메라를 찾았으므로 루프를 종료합니다.
                }
            }
        }
        else
        {
            Debug.LogError("프리팹에 Canvas 컴포넌트가 없습니다.");
        }

        canvasPrefabInstance.name = funcName + "_canvas";


        canvasPrefabInstance.transform.position = spawnPoint.position;
        canvasPrefabInstance.gameObject.SetActive(true);

        GameObject returnBtn = canvasPrefabInstance.transform.GetComponentInChildren<ReturnNodeBtn>().gameObject;
        GameObject paraBtn = canvasPrefabInstance.transform.GetComponentInChildren<ParaNodeBtn>().gameObject;
        //반환 노드 버튼 만들기
        if (hasReturn)
        {
            returnBtn.SetActive(true);
            returnBtn.GetComponent<ReturnNodeBtn>().ReturnType = returnType;

        }
        else
        {
            returnBtn.SetActive(false);
        }
        //매개변수 노드 만들기
        if (hasPara)
        {
            Debug.Log("hasPara");
            //초기화
            paraBtn.GetComponent<ParaNodeBtn>().resetParaNodeBtn();
            paraBtn.SetActive(true);

            if (hasPara1)
            {
                paraBtn.GetComponent<ParaNodeBtn>().Para1Name = para1Name;
                paraBtn.GetComponent<ParaNodeBtn>().Para1Type = para1Type;

            }
            if (hasPara2)
            {
                paraBtn.GetComponent<ParaNodeBtn>().Para2Name = para2Name;
                paraBtn.GetComponent<ParaNodeBtn>().Para2Type = para2Type;

            }
        }
        else
        {
            paraBtn.SetActive(false);
        }
    }
}


