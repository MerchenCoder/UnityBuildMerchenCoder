using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueControl_HomeScene : MonoBehaviour
{
    [SerializeField] DialogueSystem dialogueSystem;
    [SerializeField] GameObject playCanvas;
    public GameObject messagePanel;
    [SerializeField] Dialogue dialogue1;
    [SerializeField] Dialogue dialogue2;

    public GameObject setNamePanel;

    bool isFirstDiaEnd;

    void Start()
    {
        // Check First Play
        if (GameManager.Instance.CheckPlayProgress("FirstStart") == true)
        {
            isFirstDiaEnd = false;
            playCanvas.SetActive(false);
            messagePanel.SetActive(false);
            setNamePanel.SetActive(true);
            if (dialogueSystem != null)
            {
                // 다이얼로그 종료 이벤트에 대한 리스너 등록
                dialogueSystem.OnEndDialogue += PlayNextFlow;
                // Play First Dialogue
                // Invoke("StartFirstDialogue", 1f);
            }
        }
        else
        {
            setNamePanel.SetActive(false);
        }
    }

    public void StartFirstDialogue()
    {
        setNamePanel.SetActive(false);
        dialogue1.DialogueStart();
    }

    private void PlayNextFlow()
    {
        if (!isFirstDiaEnd)
        {
            StartCoroutine(FirstPlayFlow());
        }
        else
        {
            playCanvas.SetActive(true);
            messagePanel.SetActive(false);
        }
    }

    IEnumerator FirstPlayFlow()
    {
        // message alarm SFX will be added
        yield return new WaitForSeconds(1f);
        messagePanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        dialogue2.DialogueStart();
        isFirstDiaEnd = true;
        yield return null;
    }
}
