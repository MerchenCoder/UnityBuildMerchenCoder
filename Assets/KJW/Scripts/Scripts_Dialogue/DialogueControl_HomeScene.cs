using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueControl_HomeScene : MonoBehaviour
{
    [SerializeField] DialogueSystem dialogueSystem;
    [SerializeField] GameObject playCanvas;
    public GameObject messagePanel1;
    public GameObject messagePanel2;
    [SerializeField] Dialogue firstDialogue1;
    [SerializeField] Dialogue firstDialogue2;
    [SerializeField] Dialogue chap_1_5_Dialogue1;
    [SerializeField] Dialogue chap_1_5_Dialogue2;

    public GameObject setNamePanel;

    bool isFirstDiaEnd;
    bool isChap1_5_DiaEnd;

    void Start()
    {
        if (dialogueSystem != null)
        {
            // 다이얼로그 종료 이벤트에 대한 리스너 등록
            dialogueSystem.OnEndDialogue += PlayNextFlow;
        }

        // Check First Play
        if (GameManager.Instance.CheckPlayProgress("FirstStart"))
        {
            Debug.Log("FirstStart");
            isFirstDiaEnd = false;
            isChap1_5_DiaEnd = true;
            playCanvas.SetActive(false);
            setNamePanel.SetActive(true);
        }
        else if (GameManager.Instance.CheckPlayProgress("Chap1Clear"))
        {
            Debug.Log("Chap1Clear");
            isFirstDiaEnd = true;
            isChap1_5_DiaEnd = false;
            setNamePanel.SetActive(false);
            Invoke("StartChap1ClearDialogue", 0.5f);
        }
        else
        {
            setNamePanel.SetActive(false);
        }

        messagePanel1.SetActive(false);
        messagePanel2.SetActive(false);
    }

    // 첫 시작 다이얼로그 On
    // 이름 짓고 시작되어야 해서 따로 함수로 뺌
    public void StartFirstDialogue()
    {
        setNamePanel.SetActive(false);
        firstDialogue1.DialogueStart();
    }


    public void StartChap1ClearDialogue()
    {
        chap_1_5_Dialogue1.DialogueStart();
    }


    private void PlayNextFlow()
    {
        if (!isFirstDiaEnd)
        {
            StartCoroutine(FirstPlayFlow());
        }
        else if (!isChap1_5_DiaEnd)
        {
            StartCoroutine(Chap1_5_PlayFlow());
        }
        else
        {
            playCanvas.SetActive(true);
            messagePanel1.SetActive(false);
            messagePanel2.SetActive(false);
        }
    }

    IEnumerator FirstPlayFlow()
    {
        // message alarm SFX will be added
        yield return new WaitForSeconds(1f);
        messagePanel1.SetActive(true);
        yield return new WaitForSeconds(5f);
        firstDialogue2.DialogueStart();
        isFirstDiaEnd = true;
        yield return null;
    }

    IEnumerator Chap1_5_PlayFlow()
    {
        // message alarm SFX will be added
        yield return new WaitForSeconds(1f);
        playCanvas.SetActive(false);
        messagePanel2.SetActive(true);
        yield return new WaitForSeconds(5f);
        chap_1_5_Dialogue2.DialogueStart();
        isChap1_5_DiaEnd = true;
        // chap2 open
        DataManager.Instance.UpdateChapterState(2, true);
        yield return null;
    }
}
