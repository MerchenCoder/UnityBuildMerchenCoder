using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class NodeNameManager : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private string nodeName;
    public string NodeName
    {
        get { return nodeName; }
        set { nodeName = value; }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (NodeManager.Instance.deleteMode)
        {
            Debug.Log(gameObject.name.ToString() + "삭제");
            //이벤트 리스너부터 삭제
            EventTrigger eventTrigger = GetComponent<EventTrigger>();
            if (eventTrigger != null)
            {
                eventTrigger.triggers.Clear();
            }
            //게임 오브젝트 파괴
            Destroy(gameObject);
        }

    }
}

