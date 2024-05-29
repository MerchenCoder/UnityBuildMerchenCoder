using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForwardNode : MonoBehaviour, INode, IFollowFlow
{

    public string outputStr;
    private NodeNameManager nodeNameManager;
    private GameObject player;
    private PlayerControl playerControl;
    //private GameObject playerActionBubble;
    private void Start()
    {
        nodeNameManager = GetComponent<NodeNameManager>();
        nodeNameManager.NodeName = "ActionNode";

    }

    public IEnumerator Execute()
    {


        if (playerControl == null)
        {
            playerControl = GameObject.FindWithTag("ResultPanel_Bubble").transform.parent.GetChild(0).GetComponentInChildren<PlayerControl>(true);
        }



        //제출 모드일때
        if (NodeManager.Instance.Mode != "run")
        {
            yield return playerControl.MoveToBlock(playerControl.forwardBlockPos);

            yield break;
        }

        // PlayerControl playerControl = player.GetComponent<PlayerControl>();
        yield return new WaitForSeconds(0.2f);

        yield return playerControl.MoveToBlock(playerControl.forwardBlockPos);

        yield return new WaitForSeconds(0.3f);

    }

    public IEnumerator ProcessData()
    {
        throw new System.NotImplementedException();
    }

    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }
}
