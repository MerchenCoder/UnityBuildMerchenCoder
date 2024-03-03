using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeManager : MonoBehaviour
{
    //--싱글톤 생성--//
    public static NodeManager Instance { get; private set; }
    //실행할 INode 인스턴스들을 저장할 큐를 생성

    //변수 선언
    private GameObject startNode;
    private GameObject currentNode;
    private FlowoutPort currentFlowoutPort;


    private Coroutine executeCoroutine;


    //compile Error 상태
    private bool compileError;


    public event Action<bool> CompileErrorChanged;
    public void SetCompileError(bool value)
    {
        compileError = value;
        if (value)
        {
            // 이벤트 발생
            OnCompileErrorChanged(compileError);
        }


    }

    protected virtual void OnCompileErrorChanged(bool compileError)
    {
        CompileErrorChanged?.Invoke(compileError);
        if (executeCoroutine != null)
        {
            Debug.Log("모든 코루틴 중단");
            StopAllCoroutines();
        }

    }

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



    private void Awake()
    {
        compileError = false;
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


    //다음 노드 반환하는 메소드
    public GameObject NextNode(FlowoutPort flowoutPort)
    {
        if (flowoutPort.isConnected)
        {
            return flowoutPort.ConnectedPort.transform.parent.gameObject;
        }
        else
        {
            Debug.Log("Flow 연결에 문제가 있습니다.");
            SetCompileError(true);
            return null;
        }
    }





    //컴파일
    public void Run()
    {
        try
        {
            //find start node
            startNode = GameObject.FindGameObjectWithTag("startNode");
            currentNode = startNode;
            // 코루틴 실행 메서드

            executeCoroutine = StartCoroutine(ExcuteNode());

        }
        catch (NullReferenceException e)
        {
            Debug.LogError("Can't find start node / " + e.Message);
            SetCompileError(true);
            Debug.LogError(e.StackTrace);
        }
    }


    IEnumerator ExcuteNode()
    {
        //함수 노드 때문에 EndNode까지 반복문 돌리는 걸로 수정
        while (currentNode.GetComponent<NodeNameManager>().NodeName != "EndNode")
        {
            Debug.Log("실행 시작");
            Debug.Log("현재 실행 중 노드: " + currentNode.name);
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
                yield break;
            }
        }
        Debug.Log("Run Complete");
        yield return null;
    }
}
