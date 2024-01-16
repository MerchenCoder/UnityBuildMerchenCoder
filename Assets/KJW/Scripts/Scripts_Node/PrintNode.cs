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
        if (e.IsConnected)
        {
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
        }
        else
        {
            // Debug.Log(e.IsConnected);
            // dataUIText.text = "데이터";
        }
    }


    public void Execute()
    {
        //Canvas_Result가 Acitve 된 후에 할당해야 함.
        //result panel의 player는 항상 첫번째 자식이어야 함!!
        player = GameObject.FindWithTag("ResultPanel").transform.GetChild(0).gameObject;
        playerChatBubble = player.transform.GetChild(1).gameObject;



        playerChatBubble.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = stringData;
        playerChatBubble.SetActive(true);
        // Invoke("DisableChatBubbleAfterTime", 2f);
        StartCoroutine(DisableChatBubbleAfterTime(printDuration));
    }

    // private void DisableChatBubbleAfterTime(float )
    // {
    //     Debug.Log("말풍선 안보이게하기");
    //     playerChatBubble.SetActive(false);
    // }


    private IEnumerator DisableChatBubbleAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("말풍선 안보이게하기");
        playerChatBubble.SetActive(false);
        NodeManager.Instance.ExecuteNodes();
    }

    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }
}
