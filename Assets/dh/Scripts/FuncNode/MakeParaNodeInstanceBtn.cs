using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeParaNodeInstanceBtn : MonoBehaviour
{
    public int selectType = 0;
    public ParaNodeBtn paraNodeBtn;
    private Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(setParaNodeType);

    }

    void setParaNodeType()
    {
        paraNodeBtn.MakeInstance(selectType);
        transform.parent.parent.gameObject.SetActive(false);
    }
}
