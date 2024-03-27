using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
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
        public string face;
        [TextArea()] public string dialogueText;
        public Speaker GetSpeaker() {
            // Speaker Info Load
            return Resources.Load<Speaker>("Speaker/" + speaker);
        }
        public Face GetFace()
        {
            return (Face)System.Enum.Parse(typeof(Face), face);
        }
    }

    public class DialogueContainer
    {
        public EachDialogue[] dialogueList;
    }

    public DialogueContainer dialogueContainer;
    public EachDialogue[] thisIdDialogues;

    // 파일 이름으로 경로를 통해 가져오도록 수정
    private string dialogueJson;
    public string dialogueFileName;
    public int dialogueID;

   

    void Start()
    {
        dialogueSystem = GameObject.Find("Canvas_Dialogue").GetComponent<DialogueSystem>();
        string jsonFilePath = Application.dataPath + "/Data/Dialogue/" + dialogueFileName + ".json";
        dialogueJson = File.ReadAllText(jsonFilePath);
        string jsonString = "{ \"dialogueList\": " + dialogueJson + "}";
        dialogueContainer = JsonUtility.FromJson<DialogueContainer>(jsonString);
        FindDialogueByID(dialogueID);
    }


    // Need check in Inspector
    private void FindDialogueByID(int targetDiaID)
    {
        int i;
        int j = 0;
        for (i = 0; i < dialogueContainer.dialogueList.Length; i++)
        {
            if (dialogueContainer.dialogueList[i].diaID == targetDiaID)
            {
                break;
            }
        }
        while (dialogueContainer.dialogueList[i + j].diaID == targetDiaID)
        {
            // OutOfIndex 방지
            if (i + j + 1 < dialogueContainer.dialogueList.Length)
            {
                j++;
            }
            else break;
        }
            
        thisIdDialogues = new EachDialogue[j];
        for (j = 0; dialogueContainer.dialogueList[i + j].diaID == targetDiaID; j++)
        {
            if (i + j + 1 < dialogueContainer.dialogueList.Length)
            {
                thisIdDialogues[j] = dialogueContainer.dialogueList[j + i];
                thisIdDialogues[j].dialogueText = thisIdDialogues[j].dialogueText.Replace("{}", PlayerPrefs.GetString("player_name"));
            }
            else break;
        }
    }

    // Dialogue
    public void DialogueStart()
    {
        dialogueSystem.nowDialogueList = thisIdDialogues;
        dialogueSystem.dialogues.Add(gameObject);
        dialogueSystem.StartSpeak();
    }

    
}

