using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunBtn : MonoBehaviour
{

    Button button;
    public bool hasClicked;
    // Start is called before the first frame update
    void Start()
    {
        hasClicked = false;
        button = GetComponent<Button>();
        button.onClick.AddListener(RunCode);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RunCode()
    {
        if (!hasClicked)
        {
            hasClicked = true;
        }

        TestManager.Instance.SettingCurrentCase(1);
        NodeManager.Instance.ChangeMode("run");
        NodeManager.Instance.Run();
    }
}
