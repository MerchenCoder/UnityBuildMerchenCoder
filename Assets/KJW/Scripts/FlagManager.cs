using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    static public FlagManager instance;

    public string flagStr;

    DialogueSystemForTuto DialogueSystemForTuto;

    private void Awake()
    {
        // 싱글톤 패턴 적용
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        flagStr = "";
        DialogueSystemForTuto = GameObject.Find("Canvas_Tuto").GetComponent<DialogueSystemForTuto>();
    }

    public void Init()
    {
        instance = this;
    }

    public void SetFlag(string flag)
    {
        flagStr = flag;
    }

    public void OffFlag()
    {
        Debug.Log("off flag: " + flagStr);
        DialogueSystemForTuto.FlagSignal();
        flagStr = "";
    }
}
