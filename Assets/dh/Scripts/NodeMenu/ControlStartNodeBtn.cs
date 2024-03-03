using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlStartNodeBtn : MonoBehaviour
{

    Button btn;
    public GameObject targetObject;
    // Start is called before the first frame update
    void Start()
    {
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
