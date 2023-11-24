using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [NonSerialized] public DialogueSystem dialogueSystem;
    public enum Face { Normal, Smile, Sad, Cry, Angry, Surprise }

    [Serializable]
    public class EachDialogue
    {
        public int diaID;
        public int index;
        public string speaker;
        public Face face;
        [TextArea()] public string dialogueText;
        public Speaker GetSpeaker() {
            // ���ڿ��� Speaker ��ü�� ����
            return Resources.Load<Speaker>("Speaker/" + speaker);
        }
    }

    public class DialogueContainer
    {
        public EachDialogue[] dialogueList;
    }

    public DialogueContainer dialogueContainer;
    public EachDialogue[] thisIdDialogues;
    public TextAsset dialogueJson;
    public int dialogueID;

   

    void Start()
    {
        dialogueSystem = GameObject.Find("Canvas_Dialogue").GetComponent<DialogueSystem>();
        string jsonString = "{ \"dialogueList\": " + dialogueJson.text + "}";
        dialogueContainer = JsonUtility.FromJson<DialogueContainer>(jsonString);
        FindDialogueByID(dialogueID);
    }


    // ��ȭ ID�� �ش��ϴ� ��ȭ�� ã�� �Լ�
    private void FindDialogueByID(int targetDiaID)
    {
        int i;
        int j = 0;
        for(i = 0; i<dialogueContainer.dialogueList.Length; i++)
        {
            if (dialogueContainer.dialogueList[i].diaID == targetDiaID)
            {
                break;
            }
        }
        while (dialogueContainer.dialogueList[i + j].diaID == targetDiaID) j++;
        thisIdDialogues = new EachDialogue[j];
        for (j = 0; dialogueContainer.dialogueList[i + j].diaID == targetDiaID; j++)
        {
            thisIdDialogues[j] = dialogueContainer.dialogueList[j + i];
            thisIdDialogues[j].dialogueText = thisIdDialogues[j].dialogueText.Replace("{}", Resources.Load<Speaker>("Speaker/Player").speaker_name);
        }
    }

    // Dialogue
    public void DialogueStart()
    {
        dialogueSystem.nowDialogueList = thisIdDialogues;
        dialogueSystem.StartSpeak();
    }

    
}

