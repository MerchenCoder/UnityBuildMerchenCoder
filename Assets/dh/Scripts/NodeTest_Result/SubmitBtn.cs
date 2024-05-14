using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitBtn : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SubmitCode);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SubmitCode()
    {
        // 튜토리얼 플래그 추가 240513
        if (FlagManager.instance != null)
        {
            if (FlagManager.instance.flagStr == "SubmitBtn")
            {
                FlagManager.instance.OffFlag();
                NodeManager.Instance.ChangeMode("submit");
                NodeManager.Instance.Run();
            }
        }
        else
        {
            NodeManager.Instance.ChangeMode("submit");
            NodeManager.Instance.Run();
        }
    }
}
