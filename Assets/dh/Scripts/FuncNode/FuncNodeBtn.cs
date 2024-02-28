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

    private void OnEnable()
    {
        // spawnPoint = transform.GetComponentInParent<Canvas>().transform.GetChild(0).transform;

        btn = GetComponent<Button>();
        btn.onClick.RemoveAllListeners(); // 기존 리스너 제거
        btn.onClick.AddListener(MakeInstance);


    }

    private void MakeInstance()
    {
        Debug.Log("MakeInstance 함수 호출");
        if (funcNode != null)
        {
            Debug.Log(transform.GetComponentInParent<Canvas>().name);
            spawnPoint = transform.GetComponentInParent<Canvas>().transform.GetChild(0).transform;

            GameObject funcNodeInstance = Instantiate(funcNode);
            funcNodeInstance.transform.SetParent(spawnPoint, false);
            funcNodeInstance.transform.localPosition = Vector3.zero;
            funcNodeInstance.GetComponent<FuncNode>().Type = funcNode.GetComponent<FuncNode>().Type;

        }

    }
}
