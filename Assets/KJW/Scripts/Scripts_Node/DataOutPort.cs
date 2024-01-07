using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DataOutPort : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject arrowPrefab; // ȭ��ǥ UI ������
    private GameObject arrowObject;
    public GameObject connectedPort;
    private Vector3 originVector3; // ���� ��ġ��
    private Vector2 originLocalPosition;
    private Vector2 currentLocalPosition;
    private float arrowLength;
    public bool isConnected;
    private Color originColor; // inPort ���� �÷���
    private Camera uiCamera;

    void Start()
    {
        uiCamera = GameObject.Find("UI_Camera").GetComponent<Camera>();
        originVector3 = transform.position;
        originLocalPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
        isConnected = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // prefab ���� 
        if (arrowObject == null)
        {
            arrowObject = Instantiate(arrowPrefab.gameObject, transform.parent);
        }
        else arrowObject.SetActive(true);
        arrowObject.transform.position = originVector3;
        arrowObject.GetComponent<Image>().color = GetComponent<Image>().color;
        // ���� ���¿��� �ٽ� �巡��
        if (isConnected)
        {
            isConnected = false;
            connectedPort.GetComponent<Image>().color = originColor;
            connectedPort.GetComponent<Image>().raycastTarget = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // // // �̹����� ��ġ ����
        // transform.position = eventData.position;

        // Vector2 nowPos = transform.position;

        // float distance = Vector2.Distance(nowPos, originVector2);

        // arrowObject.transform.localScale = new Vector2(Vector2.Distance(nowPos, originVector2), 1);
        // arrowObject.transform.localRotation = Quaternion.Euler(0, 0, AngleInDeg(originVector2, nowPos));


        // // arrowObject의 위치 업데이트
        // arrowObject.transform.position = originVector2;

        // 화살표 머리의 위치를 UI 카메라를 사용하여 설정
        Vector3 currentPos = uiCamera.ScreenToWorldPoint(eventData.position);
        transform.position = new Vector3(currentPos.x, currentPos.y, transform.position.z);

        currentLocalPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);

        arrowLength = Vector2.Distance(originLocalPosition, currentLocalPosition);
        Debug.Log(arrowLength);
        arrowObject.transform.localScale = new Vector3(arrowLength, 1, 1);
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
            if (result.gameObject.GetComponent<BoxCollider2D>() != null)
            {
                if (result.gameObject.CompareTag(this.gameObject.tag) || (result.gameObject.CompareTag("data_all")))
                {
                    connectedPort = result.gameObject;
                    ConnectPort();
                    isConnected = true;
                    if (this.gameObject.CompareTag("data_int"))
                    {
                        connectedPort.tag = "data_int";
                        connectedPort.GetComponent<DataInPort>().InputValueInt = this.transform.parent.GetComponent<NodeData>().data_int;
                        connectedPort.GetComponent<DataInPort>().IsConnected = true;
                    }
                    if (this.gameObject.CompareTag("data_bool"))
                    {
                        connectedPort.tag = "data_bool";
                        connectedPort.GetComponent<DataInPort>().InputValueBool = this.transform.parent.GetComponent<NodeData>().data_bool;
                        connectedPort.GetComponent<DataInPort>().IsConnected = true;
                    }
                    if (this.gameObject.CompareTag("data_string"))
                    {
                        connectedPort.tag = "data_string";
                        connectedPort.GetComponent<DataInPort>().InputValueStr = this.transform.parent.GetComponent<NodeData>().data_string;
                        connectedPort.GetComponent<DataInPort>().IsConnected = true;
                    }
                }
            }
        }
        if (!isConnected)
        {
            transform.position = originVector3;
            arrowObject.SetActive(false);
            if (connectedPort != null)
            {
                if (this.gameObject.CompareTag("data_int"))
                {
                    connectedPort.GetComponent<DataInPort>().InputValueInt = 0;
                    connectedPort.GetComponent<DataInPort>().IsConnected = false;
                }
                if (this.gameObject.CompareTag("data_bool"))
                {
                    connectedPort.GetComponent<DataInPort>().InputValueBool = true;
                    connectedPort.GetComponent<DataInPort>().IsConnected = false;
                }
                if (this.gameObject.CompareTag("data_string"))
                {
                    connectedPort.GetComponent<DataInPort>().InputValueStr = null;
                    connectedPort.GetComponent<DataInPort>().IsConnected = false;
                }
                connectedPort.tag = "data_all";
                connectedPort = null;
            }
        }
    }

    void ConnectPort()
    {
        // out port ȭ��ǥ ����
        transform.position = connectedPort.transform.position;
        originColor = connectedPort.GetComponent<Image>().color;
        connectedPort.GetComponent<Image>().color = GetComponent<Image>().color;
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
