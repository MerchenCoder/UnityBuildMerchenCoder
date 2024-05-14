using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InputNodeBtn : MonoBehaviour
{
    //노드 프리랩
    public GameObject nodePrefab;
    //spawn 위치
    private Transform spawnPoint;
    public string inputNodeName;
    public int inputIndex;



    RectTransform canvasRect;
    // 캔버스 상의 가운데 위치 계산
    float centerXInCanvas;

    Button btn;
    void Start()
    {
        // scrollArea = GetComponentInParent<Canvas>().transform.GetChild(0).gameObject;
        spawnPoint = GetComponentInParent<Canvas>().transform.GetChild(0).GetChild(0).transform; //Canvas(main) 바로 첫번째 자식인 BG 게임 오브젝트
        // 현재 보이는 화면 기준으로 중앙에 놓기
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        centerXInCanvas = canvasRect.rect.width / 2f;
        // Debug.Log(centerXInCanvas);
        btn = GetComponent<Button>();
        btn.onClick.AddListener(MakeInstance);

    }

    private void MakeInstance()
    {
        if (nodePrefab != null)
        {
            // 튜토리얼 플래그 추가 240513
            if (FlagManager.instance != null)
            {
                if (FlagManager.instance.flagStr == nodePrefab.name)
                {
                    if (nodePrefab != null)
                    {
                        GameObject nodeInstance = Instantiate(nodePrefab);

                        nodeInstance.GetComponent<InputNode>().inputNodeName = inputNodeName;
                        nodeInstance.GetComponentInChildren<TextMeshProUGUI>().text = inputNodeName;
                        nodeInstance.GetComponent<InputNode>().inputIndex = inputIndex;

                        nodeInstance.transform.SetParent(spawnPoint, false);
                        Vector2 anchoredPosition = spawnPoint.GetComponent<RectTransform>().anchoredPosition;
                        float newPositionX = Mathf.Abs(anchoredPosition.x) + centerXInCanvas;
                        nodeInstance.transform.localPosition = new Vector3(newPositionX, 0, 0);

                        FlagManager.instance.OffFlag();
                    }
                }
            }
            else
            {
                GameObject nodeInstance = Instantiate(nodePrefab);

                nodeInstance.GetComponent<InputNode>().inputNodeName = inputNodeName;
                nodeInstance.GetComponentInChildren<TextMeshProUGUI>().text = inputNodeName;
                nodeInstance.GetComponent<InputNode>().inputIndex = inputIndex;

                nodeInstance.transform.SetParent(spawnPoint, false);
                Vector2 anchoredPosition = spawnPoint.GetComponent<RectTransform>().anchoredPosition;
                float newPositionX = Mathf.Abs(anchoredPosition.x) + centerXInCanvas;
                nodeInstance.transform.localPosition = new Vector3(newPositionX, 0, 0);
            }
            
        }
    }


}
