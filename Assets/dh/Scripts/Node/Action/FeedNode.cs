using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedNode : MonoBehaviour, INode, IFollowFlow
{
    public string outputStr;
    private NodeNameManager nodeNameManager;
    private GameObject player;
    private GameObject playerActionBubble;

    Animator feedAnim;
    private void Start()
    {
        nodeNameManager = GetComponent<NodeNameManager>();
        nodeNameManager.NodeName = "ActionNode";

    }

    public IEnumerator Execute()
    {
        if (NodeManager.Instance.Mode != "run")
        {
            TestManager.Instance.playerOutput.Add(outputStr);
            yield break;
        }

        if (player == null)
        {
            player = GameObject.FindWithTag("ResultPanel").transform.GetChild(0).gameObject;
            playerActionBubble = player.transform.GetChild(0).GetChild(0).gameObject;
            feedAnim = player.GetComponentInChildren<Animator>(true);
        }
        playerActionBubble.SetActive(true);
        //실행시 처리할 로직이 있는 코루틴 호출해주면 된다.
        feedAnim.SetBool("Feed", true);
        yield return new WaitForSeconds(2f);
        playerActionBubble.SetActive(false);
        feedAnim.SetBool("Feed", false);
        //출력 배열에 반영
        TestManager.Instance.playerOutput.Add(outputStr);
        yield return new WaitForSeconds(1f);
        yield return null;
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
