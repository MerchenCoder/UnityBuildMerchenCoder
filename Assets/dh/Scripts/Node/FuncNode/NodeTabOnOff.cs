using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeTabOnOff : MonoBehaviour
{
    private Animator nodeTabAni;
    private bool isTabOpen = false;
    // Start is called before the first frame update
    void Awake()
    {
        nodeTabAni = transform.parent.GetComponent<Animator>();
        this.GetComponent<Button>().onClick.AddListener(NodeTabMotion);
    }


    private void OnEnable() {
        isTabOpen = false;
        nodeTabAni.SetBool("isOpen",false);
    }

    public void NodeTabMotion(){
        if(isTabOpen){
            //닫기
            nodeTabAni.SetBool("isOpen",false);
            isTabOpen = false;
        }
        else {
            //열기
            nodeTabAni.SetBool("isOpen", true);
            isTabOpen = true;
        }
    }

    public void NodeTabClose()
    {
        if (isTabOpen)
        {
            //닫기
            nodeTabAni.SetBool("isOpen", false);
            isTabOpen = false;
        }
    }
}
