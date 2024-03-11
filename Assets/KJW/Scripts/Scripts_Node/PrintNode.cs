using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class PrintNode : MonoBehaviour, INode, IFollowFlow
{
    public GameObject inPort;
    private DataInPort dataInPort;
    private TMPro.TextMeshProUGUI dataUIText;
    private string stringData;

    // For Demo
    // public TMPro.TextMeshProUGUI chatText;

    //node name
    private NodeNameManager nameManager;


    //for execute
    private GameObject player;
    private GameObject playerChatBubble;

    private float printDuration = 2f;


    //SettingTestCase testManager;


    void Start()
    {
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "PrintNode";


        dataInPort = inPort.GetComponent<DataInPort>();
        dataUIText = inPort.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();

        // dataInPort.StateChanged += HandleStateChanged;
    }

    void HandleStateChanged(object sender, InputPortStateChangedEventArgs e)
    {
        // if (e.IsConnected)
        // {
        //     if (inPort.CompareTag("data_int"))
        //     {
        //         stringData = dataInPort.InputValueInt.ToString();
        //         // dataUIText.text = stringData;
        //         // chatText.text = stringData;
        //     }
        //     if (inPort.CompareTag("data_bool"))
        //     {
        //         if (dataInPort.InputValueBool)
        //         {
        //             stringData = "참";
        //         }
        //         else
        //         {
        //             stringData = "거짓";
        //         }
        //         // chatText.text = stringData;
        //     }
        //     if (inPort.CompareTag("data_string"))
        //     {
        //         stringData = dataInPort.InputValueStr;
        //         // chatText.text = stringData;
        //     }
        // }
        // else
        // {
        //     // Debug.Log(e.IsConnected);
        //     // dataUIText.text = "데이터";
        // }
    }


    IEnumerator INode.Execute()
    {
        if (!dataInPort.IsConnected)
        {
            Debug.Log("프린트노드 연결안됨");
            NodeManager.Instance.SetCompileError(true);

            yield return null;

        }
        else
        {
            Debug.Log("말하기 노드 데이터 가져오는 중");
            yield return dataInPort.connectedPort.SendData();
            // while (!dataInPort.connectedPort.SendData())
            // {
            //     Debug.Log("print노드에서 데이터 기다리는 중");
            // }
            if (inPort.CompareTag("data_int"))
            {
                stringData = dataInPort.InputValueInt.ToString();
                // dataUIText.text = stringData;
                // chatText.text = stringData;
            }
            if (inPort.CompareTag("data_bool"))
            {
                if (dataInPort.InputValueBool)
                {
                    stringData = "참";
                }
                else
                {
                    stringData = "거짓";
                }
                // chatText.text = stringData;
            }
            if (inPort.CompareTag("data_string"))
            {
                stringData = dataInPort.InputValueStr;
                // chatText.text = stringData;
            }

            //moder가 run인 경우에는 result panel 활성화, 아닌 경우에는 비활성화되므로 chatbubble 변경할 필요 없음
            if (NodeManager.Instance.Mode == "run")
            {
                // //Canvas_Result가 Acitve 된 후에 할당해야 함.
                // //result panel의 player는 항상 첫번째 자식이어야 함!!
                player = GameObject.FindWithTag("ResultPanel").transform.GetChild(0).gameObject;
                playerChatBubble = player.transform.GetChild(1).gameObject;
                playerChatBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = stringData;
                playerChatBubble.SetActive(true);
                // Invoke("DisableChatBubbleAfterTime", 2f);
                yield return new WaitForSeconds(printDuration);
                Debug.Log("말풍선 안보이게하기");
                playerChatBubble.SetActive(false);

                Debug.Log("asdfafaf");

            }
            //출력 배열에 반영
            TestManager.Instance.playerOutput.Add(stringData);

            yield return null;
        }

    }


    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }

    public IEnumerator ProcessData()
    {
        yield return null;
    }
}
