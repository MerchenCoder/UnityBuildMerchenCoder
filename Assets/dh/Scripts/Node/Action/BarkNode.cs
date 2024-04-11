using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarkNode : MonoBehaviour, INode, IFollowFlow
{
    public string outputStr;
    private NodeNameManager nodeNameManager;
    private GameObject player;
    private GameObject playerActionBubble;

    Animator barkAnim;
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
            barkAnim = player.GetComponentInChildren<Animator>(true);
        }
        // playerActionBubble.SetActive(true);

        barkAnim.SetBool("Bark", true);
        yield return new WaitForSeconds(2f);
        barkAnim.SetBool("Bark", false);
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
