using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class FuncNodeBtn : MonoBehaviour
{
    Button btn;

    [NonSerialized]
    public GameObject funcNode;

    private Transform spawnPoint;
    RectTransform canvasRect;
    // 캔버스 상의 가운데 위치 계산
    float centerXInCanvas;

    private void OnEnable()
    {
        // spawnPoint = transform.GetComponentInParent<Canvas>().transform.GetChild(0).transform;

        btn = GetComponent<Button>();
        btn.onClick.RemoveAllListeners(); // 기존 리스너 제거
        btn.onClick.AddListener(MakeInstance);


    }

    private void MakeInstance()
    {
        if (funcNode != null)
        {
            Debug.Log("MakeInstance 함수 호출");
            Debug.Log(transform.GetComponentInParent<Canvas>().name);
            spawnPoint = transform.GetComponentInParent<Canvas>().transform.GetChild(0).GetChild(0).transform;
            canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            centerXInCanvas = canvasRect.rect.width / 2f;
            Vector2 anchoredPosition = spawnPoint.GetComponent<RectTransform>().anchoredPosition;
            float newPositionX = Mathf.Abs(anchoredPosition.x) + centerXInCanvas;



            GameObject funcNodeInstance = Instantiate(funcNode);
            funcNodeInstance.transform.SetParent(spawnPoint, false);
            funcNodeInstance.transform.localPosition = new Vector3(newPositionX, 0, 0);
            funcNodeInstance.GetComponent<FuncNode>().Type = funcNode.GetComponent<FuncNode>().Type;

        }

    }
}
