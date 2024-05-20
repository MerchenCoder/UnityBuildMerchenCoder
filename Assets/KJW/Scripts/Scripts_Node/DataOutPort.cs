using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
public class DataOutPort : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject arrowPrefab; // ȭ��ǥ UI ������
    private GameObject arrowObject;
    public GameObject connectedPort;
    [NonSerialized]
    public Vector3 originVector3; // 포트 원래 좌표(월드 좌표계)
    [NonSerialized]
    public Vector2 originLocalPosition; //포트 원래 좌표(로컬 좌표계)
    Vector3 updatePosition; //업데이트 된 포트 좌표(월드 좌표계)
    private Vector2 updateLocalPosition; //업데이트 된 포트 좌표(로컬 좌표계)
    private float arrowLength;
    public bool isConnected;
    private Color originColor; // inPort ���� �÷���
    private Camera uiCamera;

    private NodeData parentNode;
    //소리
    //private Canvas canvas;
    private AutoAudioSetting autoAudioSetting;

    void Start()
    {
        uiCamera = GameObject.Find("UI_Camera").GetComponent<Camera>();
        // originVector3 = transform.position;
        // originLocalPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
        UpdatePosition();
        isConnected = false;

        parentNode = transform.parent.GetComponent<NodeData>();
        if (parentNode == null)
        {
            Debug.LogError("Node 오브젝트를 찾을 수 없습니다.");
        }
        connectedPort = null;
        //canvas = GetComponentInParent<Canvas>();
        //autoAudioSetting = GetComponentInParent<AutoAudioSetting>();
        autoAudioSetting = GetComponentInParent<NodeNameManager>().AutoAudioSetting;
    }

    //별도의 함수로 분리
    public void UpdatePosition()
    {
        originVector3 = transform.position;
        originLocalPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
    }

    public void UpdatePositionByScroll()
    {
        if (originVector3 != transform.position)
        {
            Debug.Log("originVector3 변경됨");
            originVector3 = transform.parent.TransformPoint(originLocalPosition);
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {

        if (arrowObject == null)
        {
            arrowObject = Instantiate(arrowPrefab.gameObject, transform.parent);
        }
        else arrowObject.SetActive(true);
        //arrowPosition = originVector3;
        arrowObject.transform.localPosition = originLocalPosition; // 수정 2/8
        arrowObject.GetComponent<Image>().color = GetComponent<Image>().color;

        if (isConnected)
        {
            isConnected = false;
            connectedPort.GetComponent<Image>().color = originColor;
            connectedPort.GetComponent<Image>().raycastTarget = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

        // 화살표 머리의 위치를 UI 카메라를 사용하여 설정
        Vector3 currentPos = uiCamera.ScreenToWorldPoint(eventData.position);
        updatePosition = new Vector3(currentPos.x, currentPos.y, transform.position.z);
        transform.position = updatePosition;

        updateLocalPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);

        DrawArrow();
    }

    //별도의 함수로 분리
    public void DrawArrow()
    {
        // Debug.Log("DrawArrow 함수 호출되어 화살표 다시 그리는 중");
        arrowLength = Vector2.Distance(originLocalPosition, updateLocalPosition);
        arrowObject.transform.localScale = new Vector3(arrowLength, 1, 1);
        // 몸통의 회전 설정
        arrowObject.transform.localRotation = Quaternion.Euler(0, 0, AngleInDeg(originVector3, updatePosition));
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // UI ��ҿ��� �浹 ���� Ȯ��
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<BoxCollider2D>() != null)
            {
                if (result.gameObject.CompareTag(this.gameObject.tag) || (result.gameObject.CompareTag("data_all")))
                {

                    if (connectedPort != null && (connectedPort != result.gameObject))
                    {
                        if (connectedPort.GetComponent<DataInPort>().isAllType)
                        {
                            connectedPort.tag = "data_all";
                        }
                        connectedPort.GetComponent<DataInPort>().IsConnected = false;
                        connectedPort = null;
                    }
                    connectedPort = result.gameObject;
                    ConnectPort();
                    isConnected = true;
                    // SendData();
                    connectedPort.GetComponent<DataInPort>().IsConnected = true;
                    return; //서로 다른 노드의 포트 겹쳐져있을 때 하나만 인식하도록 해야함(2/13추가)
                }
            }
        }
        if (!isConnected)
        {
            transform.position = originVector3;
            arrowObject.SetActive(false);
            if (connectedPort != null)
            {
                if (connectedPort.GetComponent<DataInPort>().isAllType)
                {
                    connectedPort.tag = "data_all";
                }
                connectedPort.GetComponent<DataInPort>().IsConnected = false;
                connectedPort = null;
            }
        }
    }

    //함수 삭제시에 사용되는 함수(NodeNameManager.cs에서 사용)
    public void DisConnect()
    {
        connectedPort.GetComponent<Image>().color = originColor;
        connectedPort.GetComponent<Image>().raycastTarget = true;
        transform.position = originVector3;
        arrowObject.SetActive(false);
        if (connectedPort != null)
        {
            if (connectedPort.GetComponent<DataInPort>().isAllType)
            {
                connectedPort.tag = "data_all";
            }
            connectedPort.GetComponent<DataInPort>().IsConnected = false;
            connectedPort = null;
        }
        //자신의 isConnected 변수도 업데이트 시켜줘야함(2.28 오류 발생)
        isConnected = false;
    }


    private void ConnectPort()
    {
        connectedPort.GetComponent<DataInPort>().connectedPort = this.GetComponent<DataOutPort>();

        // out port ȭ��ǥ ����
        updatePosition = connectedPort.transform.position;
        transform.position = updatePosition;
        updateLocalPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
        originColor = connectedPort.GetComponent<Image>().color;
        connectedPort.GetComponent<Image>().color = GetComponent<Image>().color;
        connectedPort.GetComponent<Image>().raycastTarget = false;

        DrawArrow();
        autoAudioSetting.OnClickSound_Index(1);

        // 튜토리얼 플래그 추가 240513
        if (FlagManager.instance != null)
        {
            if (FlagManager.instance.flagStr == "ConnectData")
            {
                FlagManager.instance.OffFlag();
            }
        }
    }

    public void ReconnectPort()
    {
        connectedPort.GetComponent<DataInPort>().connectedPort = this.GetComponent<DataOutPort>();
        updatePosition = connectedPort.transform.position;
        transform.position = updatePosition;
        updateLocalPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
        connectedPort.GetComponent<Image>().color = GetComponent<Image>().color;
        connectedPort.GetComponent<Image>().raycastTarget = false;
        DrawArrow();
    }

    public static float AngleInRad(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x);
    }

    public static float AngleInDeg(Vector3 vec1, Vector3 vec2)
    {
        return AngleInRad(vec1, vec2) * 180 / Mathf.PI;
    }

    public IEnumerator SendData()
    {
        Debug.Log("sendData()");
        //현재 노드에서 연결된 이전 노드의 SendData()를 호출
        // connectedPort.GetComponent<DataInPort>().IsError = parentNode.ErrorFlag;
        if (!parentNode.ErrorFlag)
        {
            Debug.Log(parentNode.gameObject.name.ToString() + " error flag가 꺼진상황");
            //정상적일 때
            if (this.gameObject.CompareTag("data_int"))
            {
                connectedPort.tag = "data_int";
                connectedPort.GetComponent<DataInPort>().InputValueInt = this.transform.parent.GetComponent<NodeData>().data_int;
                // connectedPort.GetComponent<DataInPort>().IsConnected = true;
            }
            if (this.gameObject.CompareTag("data_bool"))
            {
                connectedPort.tag = "data_bool";
                connectedPort.GetComponent<DataInPort>().InputValueBool = this.transform.parent.GetComponent<NodeData>().data_bool;
                // connectedPort.GetComponent<DataInPort>().IsConnected = true;
            }
            if (this.gameObject.CompareTag("data_string"))
            {
                connectedPort.tag = "data_string";
                connectedPort.GetComponent<DataInPort>().InputValueStr = this.transform.parent.GetComponent<NodeData>().data_string;
                // connectedPort.GetComponent<DataInPort>().IsConnected = true;
            }
            yield return null;
        }
        else
        {

            Debug.Log(parentNode.gameObject.name.ToString() + "의 errorFlag가 true인 상황.따라서 SendData를 할 수 없어서 먼저 processData()를 호출하여 데이터를 가져와 처리(계산)해야함");

            yield return parentNode.GetComponent<INode>().ProcessData();

        }
        // connectedPort.GetComponent<DataInPort>().IsConnected = true;

    }

}
