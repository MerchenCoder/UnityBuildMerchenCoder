using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowderNode : MonoBehaviour, INode, IFollowFlow
{
    [Header("Action")]
    public string outputStr;
    private NodeNameManager nodeNameManager;
    private GameObject player;
    private Animator powderAnim;

    [Header("DataPort")]
    public GameObject inPort;
    private DataInPort dataInPort;
    private string stringData;
    private GameObject playerChatBubble;

    // Start is called before the first frame update
    void Start()
    {
        nodeNameManager = GetComponent<NodeNameManager>();
        nodeNameManager.NodeName = "ActionNode";


        dataInPort = inPort.GetComponent<DataInPort>();
    }
    public IEnumerator Execute()
    {
        if (!dataInPort.IsConnected)
        {
            Debug.Log("데이터포트 연결 안됨");
            NodeManager.Instance.SetCompileError(true, "port");

            yield return null;
        }
        else
        {
            yield return dataInPort.connectedPort.SendData();
            stringData = dataInPort.InputValueInt.ToString();

            //실행모드일 때
            if (NodeManager.Instance.Mode == "run")
            {
                if (player == null)
                {
                    player = GameObject.FindWithTag("ResultPanel").transform.GetChild(0).gameObject;
                    playerChatBubble = GameObject.FindWithTag("ResultPanel_Bubble").transform.GetChild(0).gameObject;
                    powderAnim = player.GetComponentInChildren<Animator>(true);
                }

                powderAnim.SetBool("Powder", true);
                yield return playerChatBubble.GetComponent<ControlChatBubble>().Talk("가루 " + stringData + " 넣기");
                TestManager.Instance.playerOutput.Add(stringData + outputStr);
                powderAnim.SetBool("Powder", false);
                yield return new WaitForSeconds(0.5f);


            }
            //실행모드가 아닌 제출 모드일때
            else
            {
                TestManager.Instance.playerOutput.Add(stringData + outputStr);

                yield break;
            }
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
