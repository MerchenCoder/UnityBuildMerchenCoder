using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [NonSerialized] public DialogueSystem dialogueSystem;
    
    [Serializable]
    public class EachDialogue
    {
        // public int diaNum;
        // public int index;
        public Speaker speaker;
        public enum Face { Normal, Smile, Sad, Cry, Angry, Surprise }
        public Face face;
        [TextArea()] public string dialogueText;
    }

    //public TextAsset dialogueJson;
    //public int dialogueID;
   
    public EachDialogue[] dialogueList;

    void Start()
    {
        dialogueSystem = transform.parent.GetComponent<DialogueSystem>();
        
        //dialogueList = JsonUtility.FromJson<DialogueList>(dialogueJson.ToString());
        //Debug.Log(dialogueList);
    }



    // Dialogue
    public void DialogueStart()
    {
        dialogueSystem.nowDialogueList = dialogueList;
        dialogueSystem.StartSpeak();
    }

    
}

