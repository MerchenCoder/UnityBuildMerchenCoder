using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteModeControll : MonoBehaviour
{

    Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ChangeMode);
    }


    void ChangeMode()
    {
        NodeManager.Instance.deleteMode = !NodeManager.Instance.deleteMode;
        Debug.Log("deleMode : " + NodeManager.Instance.deleteMode.ToString());
    }
}
