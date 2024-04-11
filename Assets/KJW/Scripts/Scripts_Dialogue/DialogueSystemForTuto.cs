using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static DialogueForTuto;

public class DialogueSystemForTuto : MonoBehaviour
{
    [Header("Right Panel")]
    [SerializeField] private GameObject right_Panel;
    [SerializeField] private Text dialogue_text_R;

    [Header("ETC")]
    [SerializeField] private GameObject etc;
    [SerializeField] private GameObject infoPanel;
    public List<GameObject> dialogues;
    public InfoPagesController infoPagesController;
    [NonSerialized] public EachDialogueTuto[] nowDialogueList;

    int diaIndex;
    int diaListIndex;
    private string fullText;
    private string currentText = "";
    private bool isDoneTyping;
    private bool stopTyping;
    private Text typingText;
    public float typingSpeed = 0.05f;
    public AudioSource audioSource;

    private void Awake()
    {
        Init();
    }

    void Start()
    {
        
    }

    private void Init()
    {
        diaListIndex = 0;
        audioSource = GetComponent<AudioSource>();
        right_Panel.gameObject.SetActive(false);
        etc.SetActive(false);
        isDoneTyping = false;
        stopTyping = false;
    }

    public void StartSpeak()
    {
        diaIndex = 0;
        diaListIndex++;
        etc.SetActive(true);
        Speak(nowDialogueList[diaIndex].dialogueText, nowDialogueList[diaIndex].infoTabName);
    }

    public void NextSpeak()
    {
        if(!isDoneTyping) // 타이핑 안끝났으면 바로 출력
        {
            stopTyping = true;
            currentText = fullText;
            typingText.text = currentText;
        }
        else
        {
            if (diaIndex >= nowDialogueList.Length - 1) // 대사 끝났을 시 대화 종료
            {
                EndDialogue();
            }
            else
            {
                diaIndex++;
                Speak(nowDialogueList[diaIndex].dialogueText, nowDialogueList[diaIndex].infoTabName);
            }
        }
    }

    void Speak(string dia, string infoTabName)
    {
        isDoneTyping = false;
        stopTyping = false;
        currentText = ""; // Empty text
        fullText = dia;
        RightSpeakerActive();
        if (infoTabName == "" || infoTabName == null)
        {
            infoPanel.SetActive(false);
        }
        else
        {
            infoPanel.SetActive(true);
            infoPagesController.SetActiveTrueOnePage(infoTabName); // 튜토리얼 중인 페이지 펼치기 
        }
    }

     void RightSpeakerActive()
    {
        right_Panel.SetActive(true);
        typingText = dialogue_text_R;
        StartCoroutine(ShowText());
    }

    public void EndDialogue()
    {
        right_Panel.gameObject.SetActive(false);
        etc.SetActive(false);
        dialogues[diaListIndex - 1].gameObject.SetActive(false);
        infoPanel.SetActive(false);
        GetComponent<Canvas>().sortingOrder = 1;
    }

    IEnumerator ShowText()
    {
        int index = diaIndex;
        
        for (int i = 0; i <= fullText.Length && !stopTyping; i++)
        {
            if (index != diaIndex)
            {
                yield break;
            }

            currentText = fullText.Substring(0, i);
            typingText.text = currentText;
            // 타이핑 사운드 추가
            if (audioSource.clip != null)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }
            yield return new WaitForSecondsRealtime(typingSpeed);
            if(currentText == fullText) isDoneTyping = true;
        }
        yield return null;
    }
}
