using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintNode : MonoBehaviour
{

    public GameObject inPort;
    private DataInPort dataInPort;
    private TMPro.TextMeshProUGUI dataUIText;
    private string stringData;

    // For Demo
    public TMPro.TextMeshProUGUI chatText;

    void Start()
    {
        dataInPort = inPort.GetComponent<DataInPort>();
        dataUIText = inPort.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();

        dataInPort.StateChanged += HandleStateChanged;
    }

    void HandleStateChanged(object sender, InputPortStateChangedEventArgs e)
    {
        if (e.IsConnected)
        {
            if (inPort.CompareTag("data_int"))
            {
                stringData = dataInPort.InputValue.ToString();
                dataUIText.text = stringData;
                chatText.text = stringData;
            }
        }
        else
        {
            dataUIText.text = "µ•¿Ã≈Õ";
        }
    }

}
