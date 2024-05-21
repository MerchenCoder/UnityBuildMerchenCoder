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
    [SerializeField] Dialogue chap_1_5_Dialogue3;
    [SerializeField] Dialogue chap_2_5_Dialogue;

    public GameObject setNamePanel;

    private AudioSource audioSource;
    public AudioClip alarm;

    bool isFirstDiaEnd;
    bool isChap1_5_DiaEnd;
    bool report = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (dialogueSystem != null)
        {
            // 다이얼로그 종료 이벤트에 대한 리스너 등록
            dialogueSystem.OnEndDialogue += PlayNextFlow;
        }

        // Check First Play
        if (!GameManager.Instance.gameObject.transform.GetComponent<PlayData>().CheckClearPlayPoint("FirstStart"))
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
        else if (!isChap1_5_DiaEnd && !report)
        {
            playCanvas.SetActive(true);
        }
        else if (!isChap1_5_DiaEnd && report)
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

    // chapter2 open
    public  void ReportBtnClick()
    {
        if (GameManager.Instance.CheckPlayProgress("Chap1Clear"))
        {
            Debug.Log("Chap1Clear");
            playCanvas.SetActive(false);
            chap_1_5_Dialogue2.DialogueStart();
            report = true;
        }
        else if (GameManager.Instance.CheckPlayProgress("Chap2Clear"))
        {
            Debug.Log("Chap2Clear");
            playCanvas.SetActive(false);
            StartCoroutine(Chap2_5_PlayFlow());
            report = true;
        }
    }

    IEnumerator FirstPlayFlow()
    {
        // message audioSource SFX will be added
        audioSource.PlayOneShot(alarm);
        yield return new WaitForSeconds(1f);
        messagePanel1.SetActive(true);
        yield return new WaitForSeconds(5f);
        firstDialogue2.DialogueStart();
        isFirstDiaEnd = true;
        yield return null;
    }

    IEnumerator Chap1_5_PlayFlow()
    {
        playCanvas.SetActive(false);
        // message audioSource SFX will be added
        audioSource.PlayOneShot(alarm);
        yield return new WaitForSeconds(1f);
        messagePanel2.SetActive(true);
        yield return new WaitForSeconds(5f);
        chap_1_5_Dialogue3.DialogueStart();
        isChap1_5_DiaEnd = true;
        // chap2 open
        DataManager.Instance.UpdateChapterState(2, true);
        yield return null;
    }

    // chap2 clear (chap3 is not updated)
    IEnumerator Chap2_5_PlayFlow()
    {
        playCanvas.SetActive(false);
        chap_2_5_Dialogue.DialogueStart();
        yield return null;
    }
}
