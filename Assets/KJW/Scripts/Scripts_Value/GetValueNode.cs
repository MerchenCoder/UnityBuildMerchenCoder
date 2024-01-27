using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetValueNode : MonoBehaviour
{
    private AddValueBtn addValueAllowBtn;
    [SerializeField] private DataOutPort outPort;
    
    void Start()
    {
        addValueAllowBtn = transform.GetChild(0).GetComponent<AddValueBtn>();
    }

    public void BringValueData()
    {
        addValueAllowBtn.OnChangeValue();
        outPort.SendData();
    }

}
