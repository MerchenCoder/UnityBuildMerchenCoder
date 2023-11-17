using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Dialogue;

public class DialogueSystem : MonoBehaviour
{
    [Header("Left Panel")]
    [SerializeField] private GameObject left_Panel;
    [SerializeField] private Text left_name_text;
    [SerializeField] private Text left_dialogue_text;
    [SerializeField] private Image left_standing_image;

    [Header("Right Panel")]
    [SerializeField] private GameObject right_Panel;
    [SerializeField] private Text right_name_text;
    [SerializeField] private Text right_dialogue_text;
    [SerializeField] private Image right_standing_image;

    [Header("ETC")]
    public Dialogue[] dialogues;
    [NonSerialized] public EachDialogue[] nowDialogueList;
    public Button[] left_buttons = new Button[2];
    public Button[] right_buttons = new Button[2];

    int diaIndex;
    private string fullText;
    private string currentText = "";
    private Text typingText;
    public float typingSpeed = 0.05f;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        left_Panel.gameObject.SetActive(false);
        right_Panel.gameObject.SetActive(false);
    }

    public void StartSpeak()
    {
        diaIndex = 0;
        left_buttons[0].gameObject.SetActive(false);
        left_buttons[1].gameObject.SetActive(false);
        Speak(nowDialogueList[diaIndex].GetSpeaker(), nowDialogueList[diaIndex].dialogueText, nowDialogueList[diaIndex].face);
    }

    public void NextSpeak()
    {
        left_buttons[0].gameObject.SetActive(true);
        left_buttons[1].gameObject.SetActive(true);
        diaIndex++;
        Speak(nowDialogueList[diaIndex].GetSpeaker(), nowDialogueList[diaIndex].dialogueText, nowDialogueList[diaIndex].face);
        if (diaIndex >= nowDialogueList.Length - 1) // if It is 0th dialogue, Not active Left arrow button 
        {
            right_buttons[0].gameObject.SetActive(false);
            right_buttons[1].gameObject.SetActive(false);
        }
    }


    public void PrevSpeak()
    {
        right_buttons[0].gameObject.SetActive(true);
        right_buttons[1].gameObject.SetActive(true);
        diaIndex--;
        Speak(nowDialogueList[diaIndex].GetSpeaker(), nowDialogueList[diaIndex].dialogueText, nowDialogueList[diaIndex].face);
        if(diaIndex == 0) // if It is 0th dialogue, Not active Left arrow button 
        {
            left_buttons[0].gameObject.SetActive(false);
            left_buttons[1].gameObject.SetActive(false);
        }
    }


    void Speak(Speaker speaker, string dia, Dialogue.Face face)
    {
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
        left_name_text.text = speaker.speaker_name;
        typingText = left_dialogue_text;
        StartCoroutine(ShowText());
        left_standing_image.sprite = speaker.standing_sprites[(int)face];
    }

     void RightSpeakerActive(Speaker speaker, string dia, Dialogue.Face face)
    {
        left_Panel.SetActive(false);
        right_Panel.SetActive(true);
        right_name_text.text = speaker.speaker_name;
        typingText = right_dialogue_text;
        StartCoroutine(ShowText());
        right_standing_image.sprite = speaker.standing_sprites[(int)face];
    }

    IEnumerator ShowText()
    {
        int index = diaIndex;

        for (int i = 0; i <= fullText.Length; i++)
        {
            // 코루틴이 시작될 때의 diaIndex 값과 현재 diaIndex 값이 다르면 중지
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
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
