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

    private GameObject spawnCanvas;
    private Transform spawnPoint;
    private ControlNodeMenu controlNodeMenu;
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
            controlNodeMenu = GetComponentInParent<ControlNodeMenu>();
            if (controlNodeMenu.name == "NodeMenu")
            {
                spawnCanvas = GameObject.Find("MainCanvas").gameObject;
            }
            else
            {
                spawnCanvas = GetComponentInParent<Canvas>().gameObject;
            }

            spawnPoint = spawnCanvas.transform.GetChild(0).GetChild(0).transform; //Canvas(main) 바로 첫번째 자식인 BG 게임 오브젝트
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
