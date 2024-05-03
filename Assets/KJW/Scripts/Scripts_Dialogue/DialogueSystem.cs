using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Dialogue;

public class DialogueSystem : MonoBehaviour
{
    [Header("Left Panel")]
    [SerializeField] private GameObject left_Panel;
    [SerializeField] private Text left_name_text;
    [SerializeField] private Image left_standing_image;
    [SerializeField] private Text dialogue_text_L;

    [Header("Right Panel")]
    [SerializeField] private GameObject right_Panel;
    [SerializeField] private Text right_name_text;
    [SerializeField] private Image right_standing_image;
    [SerializeField] private Text dialogue_text_R;

    [Header("ETC")]
    [SerializeField] private GameObject etc;
    public List<GameObject> dialogues;
    [NonSerialized] public EachDialogue[] nowDialogueList;
    //public Button left_buttons;
    //public Button right_buttons;
    //public Button end_button;

    bool isPlayingDia;
    int diaIndex;
    int diaListIndex;
    private string fullText;
    private string currentText = "";
    private bool isDoneTyping;
    private bool stopTyping;
    private Text typingText;
    public float typingSpeed = 0.05f;
    public AudioSource audioSource;

    // 다이얼로그 종료 감지를 위한 이벤트
    public event UnityAction OnEndDialogue;

    void Start()
    {
        diaListIndex = 0;
        //end_button.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        left_Panel.gameObject.SetActive(false);
        right_Panel.gameObject.SetActive(false);
        etc.SetActive(false);
        isDoneTyping = false;
        stopTyping = false;
        isPlayingDia = false;
    }

    private void Update()
    {
        if (UnityEngine.Input.GetMouseButtonUp(0))
        {
            if (isPlayingDia) NextSpeak();
        }
    }


    public void StartSpeak()
    {
        isPlayingDia = true;
        diaIndex = 0;
        diaListIndex++;
        //dialogues[diaListIndex-1].gameObject.SetActive(false);
        //left_buttons.gameObject.SetActive(false);
        etc.SetActive(true);
        Speak(nowDialogueList[diaIndex].GetSpeaker(), nowDialogueList[diaIndex].dialogueText, nowDialogueList[diaIndex].GetFace());
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
            if (diaIndex >= nowDialogueList.Length - 1)
            {
                EndDialogue();
            }
            else
            {
                //end_button.gameObject.SetActive(false);
                // left_buttons.gameObject.SetActive(true);
                diaIndex++;
                Speak(nowDialogueList[diaIndex].GetSpeaker(), nowDialogueList[diaIndex].dialogueText, nowDialogueList[diaIndex].GetFace());
                //if (diaIndex >= nowDialogueList.Length - 1) // if It is 0th dialogue, Not active Left arrow button 
                //{
                //    //right_buttons.gameObject.SetActive(false);
                //    //end_button.gameObject.SetActive(true);
                //}
            }
        }
    }


    public void PrevSpeak()
    {
        ///right_buttons.gameObject.SetActive(true);
        diaIndex--;
        Speak(nowDialogueList[diaIndex].GetSpeaker(), nowDialogueList[diaIndex].dialogueText, nowDialogueList[diaIndex].GetFace());
        //if(diaIndex == 0) // if It is 0th dialogue, Not active Left arrow button 
        //{
        //    left_buttons.gameObject.SetActive(false);
        //}
    }


    void Speak(Speaker speaker, string dia, Dialogue.Face face)
    {
        isDoneTyping = false;
        stopTyping = false;
        currentText = ""; // Empty text
        fullText = dia;
        if (speaker.isPlayer) 
            LeftSpeakerActive(speaker, dia, face);
        else RightSpeakerActive(speaker, dia, face);
    }

    void LeftSpeakerActive(Speaker speaker, string dia, Dialogue.Face face)
    {
        right_Panel.SetActive(false);
        left_Panel.SetActive(true);
        // player name set
        left_name_text.text = PlayerPrefs.GetString("player_name");
        typingText = dialogue_text_L;
        StartCoroutine(ShowText());
        left_standing_image.sprite = speaker.standing_sprites[(int)face];
        left_standing_image.SetNativeSize();
    }

     void RightSpeakerActive(Speaker speaker, string dia, Dialogue.Face face)
    {
        left_Panel.SetActive(false);
        right_Panel.SetActive(true);
        right_name_text.text = speaker.speaker_name;
        typingText = dialogue_text_R;
        StartCoroutine(ShowText());
        right_standing_image.sprite = speaker.standing_sprites[(int)face];
        if (right_standing_image.sprite == null) right_standing_image.enabled = false;
        else right_standing_image.enabled = true;
        right_standing_image.SetNativeSize();
    }

    public void EndDialogue()
    {
        left_Panel.gameObject.SetActive(false);
        right_Panel.gameObject.SetActive(false);
        etc.SetActive(false);
        if(dialogues[diaListIndex - 1].TryGetComponent<DiaEndInteraction>(out DiaEndInteraction diaEndInteraction))
        {
            diaEndInteraction.EndDialogueInteraction();
        }
        OnEndDialogue?.Invoke();
        dialogues[diaListIndex - 1].gameObject.SetActive(false);
        isPlayingDia = false;
    }

    IEnumerator ShowText()
    {
        int index = diaIndex;
        
        for (int i = 0; i <= fullText.Length && !stopTyping; i++)
        {
            // �ڷ�ƾ�� ���۵� ���� diaIndex ���� ���� diaIndex ���� �ٸ��� ����
            if (index != diaIndex)
            {
                yield break;
            }

            currentText = fullText.Substring(0, i);
            typingText.text = currentText;
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
