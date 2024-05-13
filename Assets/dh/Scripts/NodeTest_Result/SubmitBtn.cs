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
        NodeManager.Instance.ChangeMode("submit");
        NodeManager.Instance.Run();
    }
}
