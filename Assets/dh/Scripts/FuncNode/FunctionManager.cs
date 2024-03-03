using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class FunctionManager : MonoBehaviour
{
    //----싱글톤 생성----// (씬 넘어갈때 destory할 예정)
    public static FunctionManager Instance { get; private set; }
    public GameObject[] functionPrefabs = new GameObject[6];
    public GameObject funcBtnPrefab;
    public Sprite[] funcBtnImgs;

    public Transform funcBtnSpawnPoint;



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
            Debug.Log(para2Name);
        }

    }

    //for create canvas instance
    public GameObject canvasFuncMakePrefab;
    public Transform spawnPoint;




    //현재 scene에서 만든 총 함수 개수 관리 (Modify function을 위함)
    public int totalFunction = 0;
    //함수 만들때 생성되는 canvas를 동적 배열에 저장
    public List<GameObject> myfuncCanvas = new List<GameObject>();

    //함수 노드 prefab의 핵심 컴포넌트
    public List<FuncNode> myfuncNodes = new List<FuncNode>();


    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    //-----씬이 언로드될 때 싱글톤 파괴-----//
    private void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }


    private void OnSceneUnloaded(Scene scene)
    {
        Debug.Log("FucntionManager 싱글톤 객체 파괴");
        Destroy(gameObject);
    }

    //-------------------------------//

    public void ResetFuncSetting()
    {
        Type = 0;
        funcName = null;
        para1Type = -1;
        para2Type = -1;
        returnType = -1;
    }



    public void CreateFunctionNode()
    {
        GameObject functionInstance;
        //인스턴스 설정
        int funcInsType = 0;
        if (type == 0)
        {

            Debug.Log("type오류");
        }

        //0 - 1번
        //1 - 2번
        //2 - 3번에 파라미터 1개 
        //3 - 4번에 파라미터 1개 
        //4 - 3번에 파라미터 2개 
        //5 - 4번에 파라미터 2개
        if (hasPara1 && hasPara2)
        {
            funcInsType = type + 2 - 1;
        }
        else
        {
            funcInsType = type - 1;
        }
        functionInstance = Instantiate(functionPrefabs[funcInsType]);
        int[] paraTypes = new int[] { para1Type, para2Type };
        string[] paraNames = new string[] { para1Name, para2Name };
        //Debug.Log(para2Name);
        //Debug.Log(paraNames);
        //functionInstance port type & function name 설정
        functionInstance = SetFuncNode(functionInstance, type, funcName, paraTypes, paraNames, returnType);


        FuncNode funcNode = functionInstance.GetComponent<FuncNode>();
        //FuncNode의 funIndex 설정
        funcNode.funIndex = myfuncNodes.Count;
        //FuncNode의 type 설정
        funcNode.Type = type;
        funcNode.funName = funcName;

        myfuncNodes.Add(funcNode);

        functionInstance.transform.SetParent(this.transform, false);

        //funcBtn prefab으로 인스턴스 생성 후 기타 설정
        GameObject funcBtn = Instantiate(funcBtnPrefab) as GameObject;
        //1. 버튼 sprite 교체
        funcBtn.GetComponent<Button>().image.sprite = funcBtnImgs[type - 1];
        //2. 버튼 text rycp
        funcBtn.GetComponentInChildren<TextMeshProUGUI>().text = funcName != null ? funcName : "이름 오류";
        //3. funBtn 버튼의 prefab gameobject 설정하기
        funcBtn.GetComponent<FuncNodeBtn>().funcNode = functionInstance;
        //funBtn 배치하기(메인 캔버스 노드 메뉴에)
        funcBtn.transform.SetParent(funcBtnSpawnPoint, false);

        //funBtn 배치하기(함수 캔버스 노드 메뉴에)
        for (int i = 0; i < myfuncCanvas.Count - 1; i++)
        {
            Transform funcNodeMenu = myfuncCanvas[i].transform.GetChild(2);

            if (funcNodeMenu != null)
            {
                Transform panelFunContent = funcNodeMenu.GetChild(9).GetChild(0).transform;

                //funcBtn 복제
                GameObject clonedFuncBtn = Instantiate(funcBtn);
                clonedFuncBtn.GetComponent<FuncNodeBtn>().funcNode = funcBtn.GetComponent<FuncNodeBtn>().funcNode;

                clonedFuncBtn.transform.SetParent(panelFunContent, false);
                Debug.Log(myfuncCanvas[i].name + "에 clonedFuncBtn 삽입");

            }
            else
            {
                Debug.Log("funcNodeMenu is null ref");
            }

        }



    }


    public GameObject SetFuncNode(GameObject funcNode, int type, string funcName, int[] paraTypes, string[] paraNames, int rtType)
    {
        funcNode.GetComponent<NodeNameManager>().NodeName = "FuncNode";
        funcNode.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = funcName;


        //outport 색상, 태그 설정
        if (type == 2 || type == 4)
        {
            GameObject outPort = funcNode.transform.GetChild(0).gameObject;
            switch (rtType)
            {
                //0:int, 1:bool, 2=string
                case 0:
                    //태그 설정
                    outPort.tag = "data_int";
                    //color 설정
                    outPort.GetComponent<Image>().color = new Color(0.949f, 0.835f, 0.290f);
                    break;
                case 1:
                    //태그 설정
                    outPort.tag = "data_bool";
                    //color 설정
                    outPort.GetComponent<Image>().color = new Color(0.651f, 0.459f, 0.965f);
                    break;
                case 2:
                    //태그 설정
                    outPort.tag = "data_string";
                    //color 설정
                    outPort.GetComponent<Image>().color = new Color(0.949f, 0.620f, 0.286f);
                    break;
            }
        }

        //inport 색상, 태그 설정
        if (type == 3 || type == 4)
        {
            DataInPort[] dataInPorts = funcNode.GetComponentsInChildren<DataInPort>();
            Debug.Log(paraTypes[0].ToString() + " " + paraTypes[1].ToString());

            if (dataInPorts.Length > 1)
            {
                for (int i = 0; i < dataInPorts.Length; i++)
                {
                    switch (paraTypes[i])
                    {
                        //0:int, 1:bool, 2=string
                        case 0:
                            //태그 설정
                            dataInPorts[i].gameObject.tag = "data_int";
                            //color 설정
                            dataInPorts[i].gameObject.GetComponent<Image>().color = new Color(0.949f, 0.835f, 0.290f, 0.5f);
                            Debug.Log("paraTypes index: " + i.ToString() + paraTypes[i].ToString());
                            Debug.Log("data_int 파라");
                            break;
                        case 1:
                            //태그 설정
                            dataInPorts[i].gameObject.tag = "data_bool";
                            //color 설정
                            dataInPorts[i].gameObject.GetComponent<Image>().color = new Color(0.651f, 0.459f, 0.965f, 0.5f);
                            Debug.Log("paraTypes index: " + i.ToString() + paraTypes[i].ToString());
                            Debug.Log("data_bool 파라");
                            break;
                        case 2:
                            //태그 설정
                            dataInPorts[i].gameObject.tag = "data_string";
                            //color 설정
                            dataInPorts[i].gameObject.GetComponent<Image>().color = new Color(0.949f, 0.620f, 0.286f, 0.5f);
                            Debug.Log("paraTypes index: " + i.ToString() + paraTypes[i].ToString());
                            Debug.Log("data_string 파라");
                            break;
                    }
                    dataInPorts[i].GetComponentInChildren<TextMeshProUGUI>().text = paraNames[i];
                }
            }
            else
            {
                if (paraTypes[0] == -1)
                {
                    switch (paraTypes[1])
                    {
                        //0:int, 1:bool, 2=string
                        case 0:
                            //태그 설정
                            dataInPorts[0].gameObject.tag = "data_int";
                            //color 설정
                            dataInPorts[0].gameObject.GetComponent<Image>().color = new Color(0.949f, 0.835f, 0.290f, 0.5f);

                            break;
                        case 1:
                            //태그 설정
                            dataInPorts[0].gameObject.tag = "data_bool";
                            //color 설정
                            dataInPorts[0].gameObject.GetComponent<Image>().color = new Color(0.651f, 0.459f, 0.965f, 0.5f);

                            break;
                        case 2:
                            //태그 설정
                            dataInPorts[0].gameObject.tag = "data_string";
                            //color 설정
                            dataInPorts[0].gameObject.GetComponent<Image>().color = new Color(0.949f, 0.620f, 0.286f, 0.5f);

                            break;
                    }
                    dataInPorts[0].GetComponentInChildren<TextMeshProUGUI>().text = paraNames[1];
                }
                else
                {
                    switch (paraTypes[0])
                    {
                        //0:int, 1:bool, 2=string
                        case 0:
                            //태그 설정
                            dataInPorts[0].gameObject.tag = "data_int";
                            //color 설정
                            dataInPorts[0].gameObject.GetComponent<Image>().color = new Color(0.949f, 0.835f, 0.290f, 0.5f);

                            break;
                        case 1:
                            //태그 설정
                            dataInPorts[0].gameObject.tag = "data_bool";
                            //color 설정
                            dataInPorts[0].gameObject.GetComponent<Image>().color = new Color(0.651f, 0.459f, 0.965f, 0.5f);

                            break;
                        case 2:
                            //태그 설정
                            dataInPorts[0].gameObject.tag = "data_string";
                            //color 설정
                            dataInPorts[0].gameObject.GetComponent<Image>().color = new Color(0.949f, 0.620f, 0.286f, 0.5f);

                            break;
                    }
                    dataInPorts[0].GetComponentInChildren<TextMeshProUGUI>().text = paraNames[0];
                }
            }


        }
        return funcNode;
    }



    public void CreateFunctionMakeCanvas()
    {
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


        //캔버스 인스턴스 이름 지정하기 : 함수이름+_canvas
        canvasPrefabInstance.name = funcName + "_canvas";

        //스폰 포지션 설정
        canvasPrefabInstance.transform.position = spawnPoint.position;
        //캔버스 활성화

        // Transform PanelFuncContent = canvasPrefabInstance.transform.Find("NodeMenu").GetChild(9).GetChild(0);

        //반환 노드, 매개변수 노드 인스턴스를 생성할 버튼 설정해주기
        GameObject returnBtn = canvasPrefabInstance.transform.GetComponentInChildren<ReturnNodeBtn>(true).gameObject;
        GameObject paraBtn = canvasPrefabInstance.transform.GetComponentInChildren<ParaNodeBtn>(true).gameObject;
        if (hasReturn || hasPara)
        {
            //canvasPrefabInstance.transform.GetChild(2).GetChild(9).gameObject.SetActive(true);
            GameObject PanelFuncContent = canvasPrefabInstance.transform.GetChild(2).GetChild(9).GetChild(0).gameObject;
            // PanelFuncContent.transform.GetChild(0).gameObject.SetActive(true);
            if (hasReturn)
            {
                returnBtn.SetActive(true);
                returnBtn.GetComponent<ReturnNodeBtn>().ReturnType = returnType;
                // returnBtn.transform.parent.gameObject.SetActive(true);

            }
            else
            {
                returnBtn.SetActive(false);
            }
            //매개변수 노드 만들기
            if (hasPara)
            {
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
            // paraBtn.transform.parent.gameObject.SetActive(true);
            Debug.Log("HorizonatalLayout 끄고 켜기");
            PanelFuncContent.GetComponent<HorizontalLayoutGroup>().enabled = false;
            PanelFuncContent.GetComponent<HorizontalLayoutGroup>().enabled = true;
            LayoutRebuilder.ForceRebuildLayoutImmediate(PanelFuncContent.GetComponent<RectTransform>());
        }
        else
        {
            canvasPrefabInstance.transform.GetChild(2).GetChild(9).GetChild(0).GetChild(0).gameObject.SetActive(false);
        }



        //함수 노드 메뉴에 다른 함수 노드 버튼 삽입하기
        Transform newCanvasPenalFunContent = canvasPrefabInstance.transform.GetChild(2).GetChild(9).GetChild(0);
        GameObject nodeMenuFuncPanel = GameObject.Find("Canvas").transform.GetChild(1).GetChild(9).GetChild(0).gameObject;
        int childCount = nodeMenuFuncPanel.transform.childCount;

        for (int i = 1; i < childCount; i++)
        {
            //현재 생성 중인 함수 제외한 함수 노드 버튼 복제
            GameObject clonedFuncBtn = Instantiate(nodeMenuFuncPanel.transform.GetChild(i).gameObject);
            clonedFuncBtn.GetComponent<FuncNodeBtn>().funcNode = nodeMenuFuncPanel.transform.GetChild(i).gameObject.GetComponent<FuncNodeBtn>().funcNode;
            Debug.Log(" 새로 생성중인 함수 캔버스에 clonedFuncBtn 삽입 - 반복 횟수 : " + i.ToString());
            clonedFuncBtn.transform.SetParent(newCanvasPenalFunContent, false);
        }

        //함수 개수 업데이트, 캔버스 리스트 업데이트
        totalFunction++;
        myfuncCanvas.Add(canvasPrefabInstance);


        canvasPrefabInstance.gameObject.SetActive(true);

    }
}


