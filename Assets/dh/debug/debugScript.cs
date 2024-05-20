using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class debugScript : MonoBehaviour, IDragHandler, IPointerClickHandler
{

    private RectTransform boundaryObject; // 이동 가능한 범위를 제한할 오브젝트
    private Vector2 boundarySize; // 이동 가능한 범위 크기
    Vector2 currentPos;

    public void OnDrag(PointerEventData eventData)
    {
        // boundaryObject = GameObject.Find("ScrollContent").GetComponent<RectTransform>();
        // RectTransformUtility.ScreenPointToLocalPointInRectangle(boundaryObject, eventData.position, eventData.pressEventCamera, out currentPos);
        // Debug.Log(currentPos);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        boundaryObject = GameObject.Find("ScrollContent").GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(boundaryObject, eventData.position, eventData.pressEventCamera, out currentPos);
        Debug.Log(currentPos);
    }
}
