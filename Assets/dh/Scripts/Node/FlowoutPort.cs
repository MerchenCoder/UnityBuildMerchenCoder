using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class FlowoutPort : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject arrowPrefab; // ȭ��ǥ UI ������
    private GameObject arrowObject;

    private GameObject connectedPort;

    public GameObject ConnectedPort
    {
        get { return connectedPort; }
        set { connectedPort = value; }
    }

    [NonSerialized]
    public Vector3 originVector3; // 포트 원래 좌표(월드 좌표계)
    [NonSerialized]
    public Vector2 originLocalPosition; //포트 원래 좌표(로컬 좌표계)

    Vector3 updatePosition; //업데이트 된 포트 좌표(월드 좌표계)
    private Vector2 updateLocalPosition; //업데이트 된 포트 좌표(로컬 좌표계)

    private float arrowLength;



    public bool isConnected;
    private Sprite nullImage;


    public bool IsConnected
    {
        get { return isConnected; }
        set { isConnected = value; }
    }

    private Camera uiCamera;



    void Start()
    {
        uiCamera = GameObject.Find("UI_Camera").GetComponent<Camera>();
        // originVector3 = transform.position;
        // originLocalPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
        UpdatePosition();
        isConnected = false;

    }

    public void UpdatePosition()
    {
        originVector3 = transform.position;
        originLocalPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        // prefab ���� 
        if (arrowObject == null)
        {
            arrowObject = Instantiate(arrowPrefab, transform.parent);
            // arrowObject.transform.position = originVector3;

        }
        else arrowObject.SetActive(true);
        arrowObject.transform.localPosition = originLocalPosition; // 수정 2/8

        // ���� ���¿��� �ٽ� �巡��
        if (isConnected)
        {
            isConnected = false;
            connectedPort.GetComponent<Image>().sprite = nullImage;
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
            if (result.gameObject.GetComponent<BoxCollider2D>() != null && (result.gameObject.CompareTag(this.gameObject.tag) || result.gameObject.CompareTag("flow_end")))
            {
                // Debug.Log("연결되었습니다.");
                connectedPort = result.gameObject;
                ConnectPort();
                isConnected = true;
                connectedPort.GetComponent<FlowinPort>().IsConnected = true;
                if (connectedPort.transform.parent.gameObject.tag == "endNode")
                {
                    connectedPort.GetComponent<endNode>().IsConnected = true;

                }
                //result.gameObject.GetComponent<endNode>().isConnectedEnd();

            }
        }
        if (!isConnected)
        {
            transform.position = originVector3;
            arrowObject.SetActive(false);
            if (connectedPort != null && connectedPort.transform.parent.gameObject.tag == "endNode")
            {
                connectedPort.GetComponent<endNode>().IsConnected = false;
            }
            connectedPort.GetComponent<FlowinPort>().IsConnected = false;
            connectedPort = null;
        }
    }

    public void ConnectPort()
    {
        connectedPort.GetComponent<FlowinPort>().connectedPort = this.GetComponent<FlowoutPort>();
        // out port ȭ��ǥ ����
        updatePosition = connectedPort.transform.position;
        transform.position = updatePosition;
        updateLocalPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);

        nullImage = connectedPort.GetComponent<Image>().sprite;
        connectedPort.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
        connectedPort.GetComponent<Image>().raycastTarget = false;

        DrawArrow();
    }

    public void ReconnectPort()
    {
        connectedPort.GetComponent<FlowinPort>().connectedPort = this.GetComponent<FlowoutPort>();
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



}