using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnNodeBtn : MonoBehaviour
{
    public GameObject returnNodePrefab;
    private Transform spawnPoint;
    Button btn;

    private void Start() {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(MakeInstance);

        spawnPoint = transform.GetComponentInParent<Canvas>().transform.GetChild(0).transform;
        Debug.Log(spawnPoint.parent); //transform.GetComponentInParent<Canvas>()의 바로 아래 자식
    }

    //returnNode 정보
    private int returnType = -1;

    public int ReturnType {
        set{returnType = value;}
        get{return returnType;}
    }

    //공통 기능이라 별도 스크립트로 뺄지 고민중..
    public void MakeInstance() {
        GameObject returnNodeInstance = Instantiate(returnNodePrefab);
        returnNodeInstance.GetComponent<ReturnNode>().SetReturnNode(returnType);
        returnNodeInstance.transform.SetParent(spawnPoint, false); // 부모를 spawnPoint로 설정하고, worldPositionStays를 false로 설정하여 로컬 좌표로 배치
        returnNodeInstance.transform.localPosition = Vector3.zero; // 로컬 좌표의 원점에 배치
    }
}
