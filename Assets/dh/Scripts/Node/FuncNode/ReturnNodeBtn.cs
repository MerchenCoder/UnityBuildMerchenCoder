using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnNodeBtn : MonoBehaviour
{
    public GameObject returnNodePrefab;
    private Transform spawnPoint;
    RectTransform canvasRect;
    float centerXInCanvas;

    Button btn;
    [Header("Audio")]
    [SerializeField] private AutoAudioSetting autoAudioSetting;


    private void Start()
    {
        if (autoAudioSetting == null)
        {
            autoAudioSetting = GetComponentInParent<AutoAudioSetting>();
        }
        btn = GetComponent<Button>();
        btn.onClick.AddListener(MakeInstance);

        spawnPoint = transform.GetComponentInParent<Canvas>().transform.GetChild(0).GetChild(0).transform;
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        centerXInCanvas = canvasRect.rect.width / 2;
    }

    //returnNode 정보
    private int returnType = -1;

    public int ReturnType
    {
        set { returnType = value; }
        get { return returnType; }
    }

    //공통 기능이라 별도 스크립트로 뺄지 고민중.. / 노드에서 각기 달리 커스텀 해야하는 부분도 존재
    public void MakeInstance()
    {
        GameObject returnNodeInstance = Instantiate(returnNodePrefab);
        returnNodeInstance.GetComponent<NodeNameManager>().NodeName = "ReturnNode";
        returnNodeInstance.GetComponent<ReturnNode>().SetReturnNode(returnType);
        returnNodeInstance.transform.SetParent(spawnPoint, false); // 부모를 spawnPoint로 설정하고, worldPositionStays를 false로 설정하여 로컬 좌표로 배치


        //scroll rect 이동을 반영해 canvas 중간 배치
        Vector2 anchoredPosition = spawnPoint.GetComponent<RectTransform>().anchoredPosition;
        float newPositionX = Mathf.Abs(anchoredPosition.x) + centerXInCanvas;
        returnNodeInstance.transform.localPosition = new Vector3(newPositionX, 0, 0);
        autoAudioSetting.OnClickSound_Index(0);//0520 사운드추가

    }
}
