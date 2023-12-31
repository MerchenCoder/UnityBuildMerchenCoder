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


    private Vector2 originVector2; // ���� ��ġ��
    private bool isConnected;
    private Sprite nullImage;


    public bool IsConnected
    {
        get { return isConnected; }
        set { isConnected = value; }
    }


    void Start()
    {
        originVector2 = transform.position;
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
        arrowObject.transform.position = originVector2;

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
        // �̹����� ��ġ ����
        transform.position = eventData.position;

        Vector2 nowPos = transform.position;
        arrowObject.transform.localScale = new Vector2(Vector2.Distance(nowPos, originVector2), 1);
        arrowObject.transform.localRotation = Quaternion.Euler(0, 0, AngleInDeg(originVector2, nowPos));
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
                Debug.Log("연결되었습니다.");
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
            transform.position = originVector2;
            arrowObject.SetActive(false);
            if (connectedPort.transform.parent.gameObject.tag == "endNode")
            {
                connectedPort.GetComponent<endNode>().IsConnected = false;
            }
        }
    }

    void ConnectPort()
    {
        // ���� ����
        arrowObject.transform.localScale = new Vector2(Vector2.Distance(connectedPort.transform.position, originVector2), 1);
        arrowObject.transform.localRotation = Quaternion.Euler(0, 0, AngleInDeg(originVector2, connectedPort.transform.position));
        // out port ȭ��ǥ ����
        transform.position = connectedPort.transform.position;
        nullImage = connectedPort.GetComponent<Image>().sprite;
        connectedPort.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
        connectedPort.GetComponent<Image>().raycastTarget = false;
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
