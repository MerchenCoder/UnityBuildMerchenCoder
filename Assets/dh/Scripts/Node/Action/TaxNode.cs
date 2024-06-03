using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaxNode : MonoBehaviour, INode, IFollowFlow
{
    [Header("Action")]
    public string outputStr;
    private NodeNameManager nodeNameManager;
    private GameObject player;
    //private Animator taxAnim;

    [Header("DataPort")]
    public GameObject inPort;
    private DataInPort dataInPort;
    private int inputData;
    private GameObject playerActionBubble;


    [Header("Data")]
    private NodeData nodeData;
    [SerializeField] private DataOutPort outPort;

    [Header("Sound")]
    public AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        nodeNameManager = GetComponent<NodeNameManager>();
        nodeNameManager.NodeName = "ActionNode";
        nodeData = GetComponent<NodeData>();

        dataInPort = inPort.GetComponent<DataInPort>();
    }


    public IEnumerator Execute()
    {
        audioSource = nodeNameManager.AutoAudioSetting.AudioSource;

        if (!dataInPort.IsConnected)
        {
            Debug.Log("데이터포트 연결 안됨");
            NodeManager.Instance.SetCompileError(true, "port");

            yield return null;
        }
        else
        {
            yield return dataInPort.connectedPort.SendData();
            inputData = dataInPort.InputValueInt;

            nodeData.SetData_Int = CalcTax(inputData);

            //실행모드일 때
            if (NodeManager.Instance.Mode == "run")
            {
                if (player == null)
                {
                    player = GameObject.FindWithTag("ResultPanel").transform.GetChild(0).gameObject;
                    playerActionBubble = player.transform.GetChild(0).GetChild(0).gameObject;
                    //taxAnim = player.GetComponentInChildren<Animator>(true);
                }
                audioSource.PlayOneShot(audioClip);
                playerActionBubble.SetActive(true);
                yield return new WaitForSeconds(1f);
                playerActionBubble.SetActive(false);
                audioSource.Stop();
                yield return new WaitForSeconds(0.5f);

            }
        }


    }

    private int CalcTax(int profit)
    {
        int tax = 0;
        if (profit >= 400000)
            tax = profit / 5;
        else if (profit >= 100000)
            tax = profit / 10;
        else tax = 0;


        nodeData.ErrorFlag = false;

        return tax;

    }

    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }

    public IEnumerator ProcessData()
    {
        if (nodeData.ErrorFlag)
        {
            NodeManager.Instance.SetCompileError(true, "실행되지 않은 액션 노드의\n 결과값을 가져올 수 없습니다.");
            yield break;
        }

        yield return outPort.SendData();
        nodeData.ErrorFlag = true;
    }

}
