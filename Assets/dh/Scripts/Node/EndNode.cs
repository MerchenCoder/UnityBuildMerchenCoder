using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndNode : MonoBehaviour, IFollowFlow, INode
{
    //node name
    private NodeNameManager nameManager;

    public outFlow NextFlow()
    {
        return null;
    }


    IEnumerator INode.Execute()
    {
        yield return null;
    }

    IEnumerator INode.ProcessData()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "EndNode";
    }

    private void Update()
    {

    }
}

