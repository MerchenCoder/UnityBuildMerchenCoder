using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class NodeManager : MonoBehaviour
{
    //--싱글톤 생성--//
    public static NodeManager Instance { get; private set; }
    //==========모드================//
    private string mode = "run";
    public string Mode
    {
        get
        {
            return mode;
        }
    }


    public void ChangeMode(string updateMode)
    {
        mode = updateMode;
    }


    //변수 선언
    private GameObject startNode;
    private GameObject currentNode;
    private FlowoutPort currentFlowoutPort;
    [SerializeField]
    public GameObject resultCanvas;

    public ResultCanvasManager resultCanvasManager;
    public GameObject npc_chatBubble;

    // public bool isInnerLoop = false;
    // public GameObject loopStartNode = null;
    // public bool isForLoop = false;
    // public bool isWhileLoop = false;

    //private Coroutine executeCoroutine;


    //========compile Error 상태==========//
    private bool compileError;
    public bool CompileError
    {
        get
        {
            return compileError;
        }
    }

    public event Action<bool> CompileErrorChanged;
    public void SetCompileError(bool value, string error)
    {
        compileError = value;
        if (value)
        {
            // 이벤트 발생
            OnCompileErrorChanged(compileError, error);
        }


    }

    protected virtual void OnCompileErrorChanged(bool compileError, string error)
    {
        CompileErrorChanged?.Invoke(compileError);
        // if (executeCoroutine != null)
        // {
        //     Debug.Log("모든 코루틴 중단");
        //     StopAllCoroutines();
        // }

        Debug.Log("모든 코루틴 중단");
        StopAllCoroutines();
        resultCanvas.GetComponent<RunErrorMsg>().SetStateStop();
        resultCanvas.GetComponent<RunErrorMsg>().ActiveErrorMsg(error);

    }



    private void Awake()
    {
        compileError = false;
        Debug.Log(compileError);
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

    private void Start()
    {
        resultCanvasManager.SetResultCanvas();
        // isInnerLoop = false;
        // loopStartNode = null;
        // isForLoop = false;
        // isWhileLoop = false;
    }

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
        Debug.Log("NodeManager 싱글톤 객체 파괴");
        Instance = null; // Instance 변수를 초기화하여 새로운 싱글톤 객체 생성을 허용
        Destroy(gameObject);

    }





    //=====삭제=========//
    private bool deleteMode = false;
    public event Action<bool> DeleteModeChanged;

    public bool DeleteMode
    {
        get
        {
            return deleteMode;

        }

        set
        {
            if (deleteMode != value)
            {
                deleteMode = value;
                OnDeleteModeChanged(deleteMode);

            }

        }


    }
    protected virtual void OnDeleteModeChanged(bool newState)
    {
        DeleteModeChanged?.Invoke(newState);

    }



    //========실행 관련============//
    //다음 노드 반환하는 메소드
    public GameObject NextNode(FlowoutPort flowoutPort)
    {
        if (flowoutPort == null)
        {
            Debug.Log("ERRRRRRRR");
            return null;
        }
        if (flowoutPort.isConnected)
        {
            return flowoutPort.ConnectedPort.transform.parent.gameObject;
        }
        else
        {
            Debug.Log("Flow 연결에 문제가 있습니다.");
            SetCompileError(true, "flow");
            return null;
        }
    }

    //컴파일
    public void Run()
    {
        // isInnerLoop = false;
        // loopStartNode = null;
        // isForLoop = false;
        // isWhileLoop = false;
        SetCompileError(false, null);
        resultCanvas.GetComponent<RunErrorMsg>().SetStateRun();
        if (mode == "run")
        {
            resultCanvas.SetActive(true);
            StartCoroutine(RunProgram());
        }
        else if (mode == "submit")
        {
            StartCoroutine(TestManager.Instance.Test());

        }
        else
        {
            Debug.Log("mode 오류 : " + mode);
        }
    }


    public IEnumerator RunProgram()
    {
        try
        {
            //find start node
            startNode = GameObject.FindGameObjectWithTag("startNode");
            currentNode = startNode;
            // 코루틴 실행 메서드

            // executeCoroutine = StartCoroutine(ExcuteNode());
        }
        catch (NullReferenceException e)
        {
            Debug.LogError("Can't find start node / " + e.Message);
            SetCompileError(true, "startNode");
            Debug.LogError(e.StackTrace);
        }


        if (mode == "run" && TestManager.Instance.testCaseData.hasTestCaseInput)
        {

            //Input 있는 경우 말하기
            string inputs = "";
            for (int i = 0; i < TestManager.Instance.currentInput.Count; i++)
            {
                inputs += TestManager.Instance.testCaseData.inputInfo[i].name + " : ";
                inputs += TestManager.Instance.currentInput[i];
                if (i != TestManager.Instance.currentInput.Count - 1)
                    inputs += ",\n";
            }

            npc_chatBubble.GetComponentInChildren<TMPro.TMP_Text>().text = inputs;
            npc_chatBubble.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            npc_chatBubble.SetActive(false);
        }

        yield return StartCoroutine(ExcuteNode()); //ExcuteNode 코루틴 호출 후 끝날때까지 기다림.
    }

    IEnumerator ExcuteNode()
    {
        //함수 노드 때문에 EndNode까지 반복문 돌리는 걸로 수정
        while (currentNode.GetComponent<NodeNameManager>().NodeName != "EndNode")
        {
            Debug.Log("실행 시작");
            Debug.Log("현재 실행 중 노드: " + currentNode.name);
            // if (currentNode.GetComponent<NodeNameManager>().NodeName == "WhileLoopNode")
            // {
            //     isInnerLoop = true;
            //     isWhileLoop = true;
            //     loopStartNode = currentNode.gameObject;
            // }
            // else if (currentNode.GetComponent<NodeNameManager>().NodeName == "ForLoopNode")
            // {
            //     isInnerLoop = true;
            //     isForLoop = true;
            //     loopStartNode = currentNode.gameObject;
            // }
            //현재 노드 실행 후 끝날 때까지 기다리기
            yield return currentNode.GetComponent<INode>().Execute();
            // if (compileError)
            // {
            //     //오류 메시지 UI 필요
            //     Debug.Log("컴파일 오류가 있어서 실행 중단");
            //     break;
            // }
            //현재 노드의 Flow outPort 찾기
            currentFlowoutPort = currentNode.GetComponent<IFollowFlow>().NextFlow();
            // Debug.Log("현재 플로우 아웃포트 " + currentFlowoutPort);
            //Flow outPort로 다음 node 찾아서 currentNode 업데이트
            currentNode = NextNode(currentFlowoutPort);
            if (currentNode == null)
            {
                Debug.Log("ExcuteNode 코루틴 종료");
                SetCompileError(true, "flow");
                yield break;
            }
        }
        //Debug.Log("Run Complete");

        if (mode == "run")
        {
            bool result = TestManager.Instance.CheckAnswer();
            if (result)
            {
                print("정답 -> 성공 애니메이션 실행");
                yield return resultCanvas.GetComponent<ControlAnimation>().Success();
            }
            else
            {
                print("오답 -> 실패 애니메이션 실행");
                yield return resultCanvas.GetComponent<ControlAnimation>().Fail();
            }
        }

        resultCanvas.GetComponent<RunErrorMsg>().SetStateComplete();

    }
}
