using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class runNode1 : MonoBehaviour
{
    public GameObject resultCanvas;
    public GameObject flowEndPort;
    public GameObject flowEndPort1;
    public GameObject flowStartPort;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (flowEndPort.GetComponent<endNode>().isConnected == true && flowStartPort.GetComponent<FlowoutPort>().IsConnected == true && flowEndPort1.GetComponent<endNode>().isConnected == true)  // isConnected 변수 대신 IsConnected 프로퍼티 사용
        {
            button.interactable = true;
            // Debug.Log("실행가능합니다.");
        }
        else
        {
            button.interactable = false;
            // Debug.Log("실행 불가능합니다.");
        }
    }

    public void Run()
    {
        resultCanvas.SetActive(true);

        NodeManager.Instance.Run();

        // if (NodeManager.Instance != null)
        // {
        //     NodeManager.Instance.ExecuteNodes();
        // }
        // else
        // {
        //     Debug.Log("NodeManager.Instance == null");
        // }
    }


}
