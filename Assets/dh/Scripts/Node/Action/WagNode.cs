using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagNode : MonoBehaviour, INode, IFollowFlow
{
    public string outputStr;
    private NodeNameManager nodeNameManager;
    private GameObject player;
    private GameObject playerActionBubble;

    Animator wagAnim;
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
            player = GameObject.FindWithTag("Player");
            wagAnim = player.GetComponentInChildren<Animator>(true);
        }
        // playerActionBubble.SetActive(true);

        wagAnim.SetBool("Wag", true);
        yield return new WaitForSeconds(2f);
        wagAnim.SetBool("Wag", false);
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
