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
        btn.onClick.AddListener(MakeInstance);


    }

    private void MakeInstance()
    {
        if (funcNode != null)
        {
            spawnPoint = transform.GetComponentInParent<Canvas>().transform.GetChild(0).transform;

            GameObject funcNodeInstance = Instantiate(funcNode);
            funcNodeInstance.transform.SetParent(spawnPoint, false);
            funcNodeInstance.transform.localPosition = Vector3.zero;
            funcNodeInstance.GetComponent<FuncNode>().Type = funcNode.GetComponent<FuncNode>().Type;
            Debug.Log(funcNodeInstance.GetComponent<FuncNode>().Type);
            Debug.Log(funcNode.GetComponent<FuncNode>().Type);

        }

    }
}
