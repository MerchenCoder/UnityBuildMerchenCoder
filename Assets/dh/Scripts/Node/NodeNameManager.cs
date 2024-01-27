using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class NodeNameManager : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private string nodeName;
    public string NodeName
    {
        get { return nodeName; }
        set { nodeName = value; }
    }

    private Canvas canvas;
    private RectTransform rectTransform;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = this.GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down");

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        DataOutPort[] dataOutPorts = GetComponentsInChildren<DataOutPort>();
        FlowoutPort[] flowoutPorts = GetComponentsInChildren<FlowoutPort>();

        foreach (DataOutPort dataOutPort in dataOutPorts)
        {
            dataOutPort.UpdatePosition();
        }
        foreach (FlowoutPort flowoutPort in flowoutPorts)
        {
            flowoutPort.UpdatePosition();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("drag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
