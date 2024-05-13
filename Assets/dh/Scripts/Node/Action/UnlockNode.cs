using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockNode : MonoBehaviour, INode, IFollowFlow
{
    [Header("Action")]
    public string outputStr;
    private NodeNameManager nodeNameManager;
    private GameObject player;
    private Animator unlockAnim;

    [Header("DataPort")]
    public GameObject inPort;
    private DataInPort dataInPort;
    private string stringData;
    private GameObject playerChatBubble;

    private void Start()
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
            if (inPort.CompareTag("data_int"))
            {
                stringData = dataInPort.InputValueInt.ToString();
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
            }
            if (inPort.CompareTag("data_string"))
            {
                stringData = dataInPort.InputValueStr;
            }

            //실행모드일 때
            if (NodeManager.Instance.Mode == "run")
            {
                if (player == null)
                {
                    player = GameObject.FindWithTag("ResultPanel").transform.GetChild(0).gameObject;
                    playerChatBubble = GameObject.FindWithTag("ResultPanel_Bubble").transform.GetChild(0).gameObject;
                    unlockAnim = player.GetComponentInChildren<Animator>(true);
                }
                foreach (char c in stringData)
                {
                    unlockAnim.SetBool("Unlock", true);
                    yield return playerChatBubble.GetComponent<ControlChatBubble>().Talk(c.ToString());
                    unlockAnim.SetBool("Unlock", false);
                    TestManager.Instance.playerOutput.Add(c.ToString());
                    yield return new WaitForSeconds(0.5f);
                }

            }
            //실행모드가 아닌 제출 모드일때
            else
            {
                foreach (char c in stringData)
                {
                    TestManager.Instance.playerOutput.Add(c.ToString());
                }
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
