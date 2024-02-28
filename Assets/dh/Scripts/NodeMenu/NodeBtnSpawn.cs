using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeMenuBtn : MonoBehaviour
{
    //노드 프리랩
    public GameObject nodePrefab;
    //spawn 위치
    private Transform spawnPoint;


    Button btn;
    void Start()
    {
        spawnPoint = GetComponentInParent<Canvas>().transform.GetChild(0).GetChild(0).transform; //Canvas(main) 바로 첫번째 자식인 BG 게임 오브젝트
        btn = GetComponent<Button>();
        btn.onClick.AddListener(MakeInstance);

    }

    private void MakeInstance()
    {
        if (nodePrefab != null)
        {
            GameObject nodeInstance = Instantiate(nodePrefab);
            nodeInstance.transform.SetParent(spawnPoint, false);
            nodeInstance.transform.localPosition = Vector3.zero;

        }
    }


}
