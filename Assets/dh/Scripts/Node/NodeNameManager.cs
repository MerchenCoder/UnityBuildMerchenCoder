using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

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

    private RectTransform boundaryObject; // 이동 가능한 범위를 제한할 오브젝트
    private Vector2 boundarySize; // 이동 가능한 범위 크기
    private Graphic uiElement;
    private Vector4 padding;
    private float raycastWidth;
    private float raycastHeight;
    private Vector2 nodeSize;

    //소리

    private Canvas canvas;
    public AutoAudioSetting autoAudioSetting;
    public AutoAudioSetting AutoAudioSetting => autoAudioSetting;




    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            autoAudioSetting = transform.parent.GetComponent<AutoAudioSetting>();
        }
        else
        {

            autoAudioSetting = canvas.GetComponentInParent<AutoAudioSetting>();
        }

        rectTransform = GetComponent<RectTransform>(); //드래그할 오브젝트
        dataOutPorts = GetComponentsInChildren<DataOutPort>();
        dataInPorts = GetComponentsInChildren<DataInPort>();
        flowoutPorts = GetComponentsInChildren<FlowoutPort>();
        flowinPorts = GetComponentsInChildren<FlowinPort>();


        uiElement = GetComponent<Graphic>();
        padding = uiElement.raycastPadding;
        nodeSize = uiElement.GetComponent<RectTransform>().rect.size;
        // raycastWidth = nodeSize.x + padding.x + padding.z; //left+right;
        // raycastHeight = nodeSize.y + padding.y + padding.w; //top+bottom;

        boundaryObject = GameObject.Find("ScrollContent").GetComponent<RectTransform>();
        if (boundaryObject != null)
        {
            SetBoundarySize();
        }
    }

    void SetBoundarySize()
    {
        // boundaryObject의 RectTransform 컴포넌트를 가져와서 크기를 가져옴
        RectTransform boundaryRectTransform = boundaryObject.GetComponent<RectTransform>();

        // boundaryObject의 크기를 가져와서 이동 가능한 범위로 설정
        //boundarySize = boundaryRectTransform.rect.size / 2f;
        boundarySize = new Vector2(boundaryObject.GetComponent<RectTransform>().rect.width / 2f, boundaryRectTransform.rect.height / 2f);//0520 수정
        //boundarySize = new Vector2(boundaryRectTransform.rect.width / 2f, boundaryRectTransform.rect.height); 
        // Debug.Log(boundarySize);
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

            //소리재생
            autoAudioSetting.OnClickSound_Index(3);

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

        // x: 왼쪽 (left) 패딩
        // y: 위쪽 (top) 패딩
        // z: 오른쪽 (right) 패딩
        // w: 아래쪽 (bottom) 패딩

        // Vector3 bottomLeft = new Vector3(rectTransform.anchoredPosition.x - nodeSize.x - padding.x, rect.yMin - padding.y, transform.position.z);
        // Vector3 bottomRight = new Vector3(rect.xMax + padding.z, rect.yMin - padding.y, transform.position.z);
        // Vector3 topLeft = new Vector3(rectTransform.anchoredPosition.x - padding.x, rect.yMax + padding.w, transform.position.z);
        // Vector3 topRight = new Vector3(rect.xMax + padding.z, rect.yMax + padding.w, transform.position.z);

        // Debug.Log($"{bottomLeft}, {bottomRight}, {topLeft}, {topRight}");

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
            //소리재생
            autoAudioSetting.OnClickSound_Index(2);
            Debug.Log("삭제모드입니다. 노드를 움직일 수 없습니다.");
            return;
        }



        //05.13.수정(미진)
        // 최상위 부모 움직이기
        Vector2 delta = eventData.delta;
        Vector2 currentPos;

        // Debug.Log(eventData)
        // 이동 가능한 범위 내에 있는지 확인
        if (boundaryObject != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(boundaryObject, eventData.position, eventData.pressEventCamera, out currentPos);

            Rect rect = rectTransform.rect;
            // float minX = rectTransform.anchoredPosition.x - nodeSize.x / 2 - padding.x;
            // float maxY = rectTransform.anchoredPosition.y + nodeSize.y / 2 - padding.w;
            // float maxX = rectTransform.anchoredPosition.x + nodeSize.x / 2 - padding.z;
            // float minY = rectTransform.anchoredPosition.y - nodeSize.y / 2 - padding.y;

            float minX = -boundarySize.x + nodeSize.x / 2 - padding.x;
            float maxX = boundarySize.x - nodeSize.x / 2 - padding.z;
            float minY = -boundarySize.y + nodeSize.y / 2 - padding.y;
            float maxY = boundarySize.y - nodeSize.y / 2 - padding.w;



            Vector2 newAnchorPos = rectTransform.anchoredPosition + delta;

            // float clampedX = Mathf.Clamp(newAnchorPos.x, -boundarySize.x, boundarySize.x);
            // float clampedY = Mathf.Clamp(newAnchorPos.y, -boundarySize.y, boundarySize.y);
            float clampedX = Mathf.Clamp(newAnchorPos.x, minX, maxX);
            float clampedY = Mathf.Clamp(newAnchorPos.y, minY, maxY);

            newAnchorPos.x = clampedX;
            newAnchorPos.y = clampedY;
            // bool isInsideWidth = (minX > -boundarySize.x) && (maxX < boundarySize.x);
            // bool isInsideHeight = (minY > -boundarySize.y) && (maxY < boundarySize.y);
            // Debug.Log($"{isInsideWidth},{isInsideHeight}");

            // if (isInsideHeight && isInsideWidth)
            // {
            //     transform.GetComponent<RectTransform>().anchoredPosition += delta;
            // }
            rectTransform.anchoredPosition = newAnchorPos;

            //기존코드 주석처리 0522=======///
            // // 이벤트 데이터의 위치를 boundaryObject의 로컬 좌표계로 변환
            // RectTransformUtility.ScreenPointToLocalPointInRectangle(boundaryObject, eventData.position, eventData.pressEventCamera, out currentPos);

            // //if (currentPos.x >= 2 * boundarySize.x || currentPos.x <= 100f || currentPos.y <= -boundarySize.y || currentPos.y >= boundarySize.y - 90f)
            // if (currentPos.x > boundarySize.x || currentPos.x < 0 || currentPos.y < -boundarySize.y || currentPos.y > boundarySize.y)//0520 수정
            // {
            //     return;
            // }
            // // 이동 가능한 범위 내에서만 이동하도록 제한합니다.
            // // float clampedX = Mathf.Clamp(currentPos.x, 100f, 2 * boundarySize.x);
            // //float clampedY = Mathf.Clamp(currentPos.y, -boundarySize.y, boundarySize.y - 90f);

            // // 이동 가능한 범위 내에서 마우스 포인터 위치로 이미지의 위치를 설정합니다.
            // transform.GetComponent<RectTransform>().anchoredPosition += delta;
            // //new Vector3(clampedX, clampedY, transform.localPosition.z);
            // // 이벤트 데이터의 위치를 boundaryObject의 로컬 좌표계로 변환
            // RectTransformUtility.ScreenPointToLocalPointInRectangle(boundaryObject, eventData.position, eventData.pressEventCamera, out currentPos);

            // //if (currentPos.x >= 2 * boundarySize.x || currentPos.x <= 100f || currentPos.y <= -boundarySize.y || currentPos.y >= boundarySize.y - 90f)
            // if (currentPos.x > boundarySize.x || currentPos.x < 0 || currentPos.y < -boundarySize.y || currentPos.y > boundarySize.y)//0520 수정
            // {
            //     return;
            // }
            // // 이동 가능한 범위 내에서만 이동하도록 제한합니다.
            // // float clampedX = Mathf.Clamp(currentPos.x, 100f, 2 * boundarySize.x);
            // //float clampedY = Mathf.Clamp(currentPos.y, -boundarySize.y, boundarySize.y - 90f);

            // // 이동 가능한 범위 내에서 마우스 포인터 위치로 이미지의 위치를 설정합니다.
            // transform.GetComponent<RectTransform>().anchoredPosition += delta;
        }






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

    public void NodeComponentClickSound()
    {
        autoAudioSetting.OnClickSound_Index(9);
    }


}



