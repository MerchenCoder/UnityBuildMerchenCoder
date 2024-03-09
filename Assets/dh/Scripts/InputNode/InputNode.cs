using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InputNode : MonoBehaviour, INode
{
    private NodeNameManager nameManager;
    private NodeData nodeData;
    public string inputNodeName;
    public int inputNumber;



    // Start is called before the first frame update
    private void Awake()
    {
        nameManager = GetComponent<NodeNameManager>();
        nameManager.NodeName = "InputNode";
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


    }

    public IEnumerator Execute()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator ProcessData()
    {
        throw new System.NotImplementedException();
    }

}
