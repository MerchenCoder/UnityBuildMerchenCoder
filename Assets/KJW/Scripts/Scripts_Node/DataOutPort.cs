using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DataOutPort : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject arrowPrefab; // 화살표 UI 프리팹
    private GameObject arrowObject;
    private GameObject connectedPort;
    private Vector2 originVector2; // 원래 위치값
    private bool isConnected;

    void Start()
    {
        originVector2 = transform.position;
        isConnected = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // prefab 생성 
        if(arrowObject == null)
        {
            arrowObject = Instantiate(arrowPrefab.gameObject, transform.parent);
        }
        else arrowObject.SetActive(true);
        arrowObject.transform.position = originVector2;

        // 연결 상태에서 다시 드래그
        if (isConnected)
        {
            isConnected = false;
            connectedPort.GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
            connectedPort.GetComponent<Image>().raycastTarget = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 이미지의 위치 설정
        transform.position = eventData.position;

        Vector2 nowPos = transform.position;
        arrowObject.transform.localScale = new Vector2(Vector2.Distance(nowPos, originVector2), 1);
        arrowObject.transform.localRotation = Quaternion.Euler(0, 0, AngleInDeg(originVector2, nowPos));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // UI 요소와의 충돌 여부 확인
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<BoxCollider2D>() != null && result.gameObject.CompareTag(this.gameObject.tag))
            {
                Debug.Log("맞음");
                connectedPort = result.gameObject;
                ConnectPort();
                isConnected = true;
            }
        }
        if(!isConnected)
        {
            transform.position = originVector2;
            arrowObject.SetActive(false);
        }
    }

    void ConnectPort()
    {
        // 라인 고정
        arrowObject.transform.localScale = new Vector2(Vector2.Distance(connectedPort.transform.position, originVector2), 1);
        arrowObject.transform.localRotation = Quaternion.Euler(0, 0, AngleInDeg(originVector2, connectedPort.transform.position));
        // out port 화살표 고정
        transform.position = connectedPort.transform.position;
        connectedPort.GetComponent<Image>().color = new Color(1, 1, 1, 1);
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
