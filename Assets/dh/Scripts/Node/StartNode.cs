using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNode : MonoBehaviour, IFollowFlow, INode
{
    //node name
    private NodeNameManager nameManager;

    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }

    IEnumerator INode.Execute()
    {
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "StartNode";
    }

}
