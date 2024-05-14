using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class NodeNameManager : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IPointerUpHandler
{
    private string nodeName;
    public string NodeName
    {
        get { return nodeName; }
        set { nodeName = value; }
    }


    private RectTransform rectTransform;
    private List<Vector3> offsetList = new List<Vector3>();


    private DataOutPort[] dataOutPorts;
    private FlowoutPort[] flowoutPorts;
    private FlowinPort[] flowinPorts;
    private DataInPort[] dataInPorts;


    [SerializeField]
    private bool canDelete = false;






    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        dataOutPorts = GetComponentsInChildren<DataOutPort>();
        dataInPorts = GetComponentsInChildren<DataInPort>();
        flowoutPorts = GetComponentsInChildren<FlowoutPort>();
        flowinPorts = GetComponentsInChildren<FlowinPort>();
    }


    public void DeleteNode()
    {
        if (canDelete)
        {
            //dataOutPort 정리
            foreach (DataOutPort dataOutPort in dataOutPorts)
            {
                if (dataOutPort.isConnected)
                {
                    dataOutPort.DisConnect();
                }
            }
            //flowoutPort 정리
            foreach (FlowoutPort flowoutPort in flowoutPorts)
            {
                if (flowoutPort.isConnected)
                {
                    flowoutPort.DisConnect();
                }
            }
            //datainPort 정리
            foreach (DataInPort dataInPort in dataInPorts)
            {
                if (dataInPort.IsConnected)
                {
                    dataInPort.connectedPort.DisConnect();
                }
            }

            //flowinPort 정리
            foreach (FlowinPort flowinPort in flowinPorts)
            {
                if (flowinPort.IsConnected)
                {
                    flowinPort.connectedPort.DisConnect();
                }
            }

            Debug.Log("포트 모두 연결 해제");
            Debug.Log(gameObject.name.ToString() + "삭제");
            //이벤트 리스너부터 삭제
            EventTrigger eventTrigger = GetComponent<EventTrigger>();
            if (eventTrigger != null)
            {
                eventTrigger.triggers.Clear();
            }


            // 튜토리얼 플래그 추가 240513
            if (FlagManager.instance != null)
            {
                if (FlagManager.instance.flagStr == "DeleteNode")
                {
                    FlagManager.instance.OffFlag();
                }
            }


            //게임 오브젝트 파괴
            Destroy(gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //삭제 로직
        if (NodeManager.Instance.DeleteMode)
        {
            if (eventData.pointerPressRaycast.gameObject.GetComponent<NodeNameManager>() != null)
            {
                canDelete = true;
            }
            else
            {
                canDelete = false;
            }

        }

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!NodeManager.Instance.DeleteMode)
        {
            // Debug.Log(eventData.pointerPressRaycast);
            if (!eventData.pointerPressRaycast.gameObject.GetComponent<NodeNameManager>())
            { //null이면 (node가 아닌 그 하위 gameobject들이 이에 해당)
                Debug.Log(eventData.pointerPressRaycast.gameObject.name + "is not Node");
                return;
            }
            foreach (Transform child in rectTransform)
            {
                if (child.GetComponent<DataOutPort>())
                {
                    Vector3 offset = child.GetComponent<DataOutPort>().originVector3 - transform.position;
                    offsetList.Add(offset);
                }
                else if (child.GetComponent<FlowoutPort>()

                )
                {
                    Vector3 offset = child.GetComponent<FlowoutPort>().originVector3 - transform.position;
                    offsetList.Add(offset);
                }
                else
                {
                    // Debug.Log("dataoutport,flowoutport가 아닌 자식 " + child.name);
                    offsetList.Add(Vector2.zero);

                }

            }
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!eventData.pointerPressRaycast.gameObject.GetComponent<NodeNameManager>())
        { //null이면 (node가 아닌 그 하위 gameobject들이 이에 해당)
            //Debug.Log(eventData.pointerPressRaycast.gameObject.name + "is not Node");
            return;
        }
        //삭제모드이면 drag시 아무런 처리 안함
        if (NodeManager.Instance.DeleteMode)
        {
            Debug.Log("삭제모드입니다. 노드를 움직일 수 없습니다.");
            return;
        }

        //최상위 부모 움직이기
        Vector2 delta = eventData.delta;
        rectTransform.anchoredPosition += delta;

        for (int i = 0; i < rectTransform.childCount; i++)
        {
            Transform child = rectTransform.GetChild(i);
            if (child != null)
            {
                if (child.GetComponent<DataOutPort>())
                {
                    if (!child.GetComponent<DataOutPort>().isConnected)
                    { //연결 안된 상태
                      // Debug.Log("connected 상태 : " + child.GetComponent<DataOutPort>().isConnected);
                      // child.GetComponent<RectTransform>().anchoredPosition += delta;
                        child.GetComponent<DataOutPort>().UpdatePosition();
                    }
                    else
                    {
                        // Debug.Log("connected 상태 : " + child.GetComponent<DataOutPort>().isConnected + "이므로 이 포트는 움직이지 않아야함");

                        //연결된 상태
                        //1. DataOutPort의 originVector3와 originLocalPosition을 update 해준다.(노드가 포트의 원래 위치도 움직임에 따라 같이 움직여야 하기 때문)
                        // Debug.Log("포트의 원래 좌표-월드계 업데이트");
                        child.GetComponent<DataOutPort>().originVector3 = transform.position + offsetList[i];

                        //2.포트는 연결되어 있는 상태에서 그 위치 그대로 유지
                        //3. arrow 다시 그리기
                        //이를 위한 함수 호출;

                        child.GetComponent<DataOutPort>().ReconnectPort();
                    }
                }
                else if (child.GetComponent<FlowoutPort>())
                {
                    if (child.GetComponent<FlowoutPort>())
                    {
                        if (!child.GetComponent<FlowoutPort>().isConnected)
                        { //연결 안된 상태
                          // Debug.Log("connected 상태 : " + child.GetComponent<FlowoutPort>().isConnected);
                          // child.GetComponent<RectTransform>().anchoredPosition += delta;
                            child.GetComponent<FlowoutPort>().UpdatePosition();
                        }
                        else
                        {
                            // Debug.Log("connected 상태 : " + child.GetComponent<FlowoutPort>().isConnected + "이므로 이 포트는 움직이지 않아야함");
                            //연결된 상태
                            //1.DataOutPort의 originVector3와 originLocalPosition을 update 해준다.(노드가 포트의 원래 위치도 움직임에 따라 같이 움직여야 하기 때문)
                            child.GetComponent<FlowoutPort>().originVector3 = transform.position + offsetList[i];

                            //2. 포트는 연결되어 있는 상태에서 그 위치 그대로 유지
                            //3. arrow 다시 그리기
                            //이를 위한 함수 호출
                            child.GetComponent<FlowoutPort>().ReconnectPort();
                        }
                    }

                }

            }





            //datainport들도 같이 움직여야 함
            DataInPort[] dataInPorts = GetComponentsInChildren<DataInPort>();
            foreach (DataInPort dataInPort in dataInPorts)
            {
                if (dataInPort.IsConnected)
                {

                    dataInPort.connectedPort.ReconnectPort();
                }
            }

            //
            FlowinPort[] flowinPorts = GetComponentsInChildren<FlowinPort>();
            foreach (FlowinPort flowinPort in flowinPorts)
            {
                if (flowinPort.IsConnected)
                {

                    flowinPort.connectedPort.ReconnectPort();
                }


            }

            // 튜토리얼 플래그 추가 240513
            if (FlagManager.instance != null)
            {
                if (FlagManager.instance.flagStr == "MoveNode")
                {
                    FlagManager.instance.OffFlag();
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerCurrentRaycast);
        if (NodeManager.Instance.DeleteMode)
        {
            if (canDelete && (eventData.pointerCurrentRaycast.gameObject == eventData.pointerPressRaycast.gameObject) && eventData.pointerCurrentRaycast.gameObject.GetComponent<NodeNameManager>() != null)
            {
                DeleteNode();
            }
            else
            {
                canDelete = false;
            }
        }
    }
}



