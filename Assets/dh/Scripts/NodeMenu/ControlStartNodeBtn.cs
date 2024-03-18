using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlStartNodeBtn : MonoBehaviour
{

    Button btn;
    private GameObject targetObject;
    // Start is called before the first frame update
    void Start()
    {
        targetObject = GetComponentInParent<Canvas>().transform.GetChild(0).GetChild(0).gameObject;
        btn = GetComponent<Button>();
        btn.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject.GetComponentInChildren<StartNode>() != null)
        {
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }
    }
}
