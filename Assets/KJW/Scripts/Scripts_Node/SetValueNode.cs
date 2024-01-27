using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetValueNode : MonoBehaviour, INode, IFollowFlow
{
    public GameObject inPort;
    private ValueManager valueManager;
    private TMP_Dropdown dropdown;
    private DataInPort dataInPort;

    private TMPro.TextMeshProUGUI dataUIText;
    private string stringData;

    //node name
    private NodeNameManager nameManager;

    void Start()
    {
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "SetValueNode";

        valueManager = GameObject.Find("ValueManager").GetComponent<ValueManager>();
        dropdown = transform.GetChild(0).GetComponent<TMP_Dropdown>();
        dataInPort = inPort.GetComponent<DataInPort>();
        dataUIText = inPort.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();

        dataInPort.StateChanged += HandleStateChanged;
    }

    public void Execute()
    {
        if(dropdown.value <= 1)
        {
            // can not play
        }
        else
        {
            if (CompareTag("data_int"))
            {
                valueManager.intValues[dropdown.value - 2].valueOfValue = dataInPort.InputValueInt;
            }
            else if (CompareTag("data_bool"))
            {
                valueManager.boolValues[dropdown.value - 2].valueOfValue = dataInPort.InputValueBool;
            }
            else if (CompareTag("data_string"))
            {
                valueManager.stringValues[dropdown.value - 2].valueOfValue = dataInPort.InputValueStr;
            }
        }
    }
    void HandleStateChanged(object sender, InputPortStateChangedEventArgs e)
    {
        if (e.IsConnected)
        {
            if (inPort.CompareTag("data_int"))
            {
                stringData = dataInPort.InputValueInt.ToString();
                dataUIText.text = stringData;
            }
            if (inPort.CompareTag("data_bool"))
            {
                if (dataInPort.InputValueBool)
                {
                    dataUIText.text = "참";
                }
                else
                {
                    dataUIText.text = "거짓";
                }
            }
            if (inPort.CompareTag("data_string"))
            {
                dataUIText.text = dataInPort.InputValueStr;
            }
        }
        else
        {
            dataUIText.text = "데이터";
        }
    }


    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }
<<<<<<< Updated upstream
=======

    IEnumerator INode.Execute()
    {
        if (dropdown.value <= 1)
        {
            // can not play
            Debug.Log("NOT SET");
        }
        else
        {
            if (CompareTag("data_int"))
            {
                valueManager.intValues[dropdown.value - 2].valueOfValue = dataInPort.InputValueInt;
            }
            else if (CompareTag("data_bool"))
            {
                valueManager.boolValues[dropdown.value - 2].valueOfValue = dataInPort.InputValueBool;
            }
            else if (CompareTag("data_string"))
            {
                valueManager.stringValues[dropdown.value - 2].valueOfValue = dataInPort.InputValueStr;
            }
        }
        yield return null;
    }
>>>>>>> Stashed changes
}
