using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    void Start()
    {
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "PrintNode";


        dataInPort = inPort.GetComponent<DataInPort>();
        dataUIText = inPort.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();

        dataInPort.StateChanged += HandleStateChanged;
    }

    void HandleStateChanged(object sender, InputPortStateChangedEventArgs e)
    {
        // if (e.IsConnected)
        // {
        //     if (inPort.CompareTag("data_int"))
        //     {
        //         stringData = dataInPort.InputValueInt.ToString();
        //         dataUIText.text = stringData;
        //         // chatText.text = stringData;
        //     }
        //     if (inPort.CompareTag("data_bool"))
        //     {
        //         if (dataInPort.InputValueBool)
        //         {
        //             dataUIText.text = "참";
        //         }
        //         else
        //         {
        //             dataUIText.text = "거짓";
        //         }
        //         // chatText.text = stringData;
        //     }
        //     if (inPort.CompareTag("data_string"))
        //     {
        //         dataUIText.text = dataInPort.InputValueStr;
        //         // chatText.text = stringData;
        //     }
        // }
        // else
        // {
        //     Debug.Log(e.IsConnected);
        //     dataUIText.text = "데이터";
        // }
    }


    public void Execute()
    {
        //Canvas_Result가 Acitve 된 후에 할당해야 함.
        //result panel의 player는 항상 첫번째 자식이어야 함!!
        player = GameObject.FindWithTag("ResultPanel").transform.GetChild(0).gameObject;
        playerChatBubble = player.transform.GetChild(1).gameObject;



        playerChatBubble.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = stringData;
        playerChatBubble.SetActive(true);

        //코루틴
        StartCoroutine(DisableChatBubbleAfterTime(2.0f)); //2초 후에 비활성화

    }


    //일정 시간 후 chat bubble 비활성화하는 코루틴
    private IEnumerator DisableChatBubbleAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerChatBubble.SetActive(false);
    }

    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }
}
