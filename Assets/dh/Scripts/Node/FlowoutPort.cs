using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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


    private Vector3 originVector3; // ���� ��ġ��
    private Vector2 originLocalPosition;
    private Vector2 currentLocalPosition;
    private float arrowLength;



    private bool isConnected;
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
        arrowObject.transform.position = originVector3;

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
        transform.position = new Vector3(currentPos.x, currentPos.y, transform.position.z);

        currentLocalPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);

        arrowLength = Vector2.Distance(originLocalPosition, currentLocalPosition);
        // Debug.Log(arrowLength);
        arrowObject.transform.localScale = new Vector3(arrowLength, 1, 1);
        //arrowObject.GetComponent<RectTransform>().sizeDelta = new Vector2(distance, arrowObject.GetComponent<RectTransform>().sizeDelta.y);


        // 몸통의 회전 설정
        arrowObject.transform.localRotation = Quaternion.Euler(0, 0, AngleInDeg(originVector3, currentPos));
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
        }
    }

    void ConnectPort()
    {
        // out port ȭ��ǥ ����
        transform.position = connectedPort.transform.position;
        nullImage = connectedPort.GetComponent<Image>().sprite;
        connectedPort.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
        connectedPort.GetComponent<Image>().raycastTarget = false;

        // ���� ����
        arrowObject.transform.localScale = new Vector2(Vector2.Distance(originLocalPosition, transform.localPosition), 1);
        arrowObject.transform.localRotation = Quaternion.Euler(0, 0, AngleInDeg(originVector3, connectedPort.transform.position));
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