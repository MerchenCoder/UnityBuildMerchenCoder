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
        // dataOutPorts = new DataOutPort[dataInPorts.Length];
        // for (int i = 0; i < dataInPorts.Length; i++)
        // {
        //     if (dataInPorts[i].IsConnected)
        //     {
        //         //연결되어있다면 연결된 dataOutPort를 찾아서 배열에 넣어주기
        //         List<RaycastResult> results = new List<RaycastResult>();
        //         EventSystem.current.RaycastAll(eventData, results);
        //         foreach (RaycastResult result in results)
        //         {

        //             if (result.gameObject.CompareTag(this.gameObject.tag))
        //             {
        //                 dataOutPorts[i] = result.gameObject.transform.GetComponent<DataOutPort>();
        //                 break;
        //             }
        //         }
        //     }
        // }


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



        // //연결된 dataOutPort 위치 업데이트
        // foreach (DataOutPort dataoutport in dataOutPorts)
        // {
        //     if (dataoutport != null)
        //     {
        //         dataoutport.transform.position = dataoutport.connectedPort.transform.position;
        //         dataoutport.UpdatePosition();
        //     }
        // }




    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        DataInPort[] dataInPorts = GetComponentsInChildren<DataInPort>();
        foreach (DataInPort dataInPort in dataInPorts)
        {
            if (dataInPort.connectedPort != null)
            {

                dataInPort.connectedPort.ConnectPort();
            }
        }


        FlowinPort[] flowinPorts = GetComponentsInChildren<FlowinPort>();
        foreach (FlowinPort flowinPort in flowinPorts)
        {
            if (flowinPort.connectedPort != null)
            {

                flowinPort.connectedPort.ConnectPort();
            }
        }

    }
}
