using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueForTuto : MonoBehaviour
{
    [NonSerialized] public DialogueSystemForTuto dialogueSystem;

    [Serializable]
    public class EachDialogueTuto
    {
        public string diaID;
        public int index;
        public string interaction; // 튜토 인터렉션
        public string infoTabName; // 대화하면서 펼쳐둘 InfoBook tab 이름
        [TextArea()] public string dialogueText;
    }

    public class DialogueContainer
    {
        public EachDialogueTuto[] dialogueList;
    }

    public DialogueContainer dialogueContainer;
    public EachDialogueTuto[] thisIdDialogues;

    // 파일 이름으로 경로를 통해 가져오도록 수정
    private string dialogueJson;
    public string dialogueFileName;
    private string dialogueID;

    // 미션 번호와 튜토리얼 ID가 일치할 때만 대화 불러오기
    bool existDia = false;

    private void Start()
    {
        Init();   
    }

    public void Init()
    {
        dialogueID = GameManager.Instance.missionData.missionCode;
        dialogueSystem = GameObject.Find("Canvas_Tuto").GetComponent<DialogueSystemForTuto>();
        //string jsonFilePath = Application.dataPath + "/Data/" + dialogueFileName + ".json";
        string jsonFilePath = Application.persistentDataPath + "/static/Dialogue/" + dialogueFileName + ".json";
        dialogueJson = File.ReadAllText(jsonFilePath);
        string jsonString = "{ \"dialogueList\": " + dialogueJson + "}";
        dialogueContainer = JsonUtility.FromJson<DialogueContainer>(jsonString);
        FindDialogueByID(dialogueID);
    }



    // Need check in Inspector
    private void FindDialogueByID(string targetDiaID)
    {
        int i;
        int j = 0;
        for (i = 0; i < dialogueContainer.dialogueList.Length; i++)
        {
            if (dialogueContainer.dialogueList[i].diaID == targetDiaID)
            {
                existDia = true;
                break;
            }
        }
        if (existDia)
        {
            while (dialogueContainer.dialogueList[i + j].diaID == targetDiaID)
            {
                // OutOfIndex 방지
                if (i + j + 1 < dialogueContainer.dialogueList.Length)
                {
                    j++;
                }
                else break;
            }
            thisIdDialogues = new EachDialogueTuto[j];
            for (j = 0; dialogueContainer.dialogueList[i + j].diaID == targetDiaID; j++)
            {
                if (i + j + 1 < dialogueContainer.dialogueList.Length)
                {
                    thisIdDialogues[j] = dialogueContainer.dialogueList[j + i];
                    thisIdDialogues[j].dialogueText = thisIdDialogues[j].dialogueText.Replace("{}", PlayerPrefs.GetString("player_name"));
                }
                else break;
            }
            // 다이얼로그 실행
            DialogueStart();
        }
        else FlagManager.instance = null;
        //else transform.parent.GetComponent<Canvas>().sortingOrder = 1;
    }

    // Dialogue
    public void DialogueStart()
    {
        Debug.Log("대화 실행");
        if (dialogueSystem == null)
        {
            dialogueSystem = GameObject.Find("Canvas_Tuto").GetComponent<DialogueSystemForTuto>();
        }
        dialogueSystem.dialogues.Add(gameObject);
        dialogueSystem.nowDialogueList = thisIdDialogues;
        dialogueSystem.StartSpeak();
    }
}

