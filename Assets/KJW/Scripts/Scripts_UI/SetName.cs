using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetName : MonoBehaviour
{
    public InputField inputField;
    public DialogueControl_HomeScene DialogueControl_HomeScene;

    public void SetButtonDown()
    {
        GameManager.Instance.playerData.name = inputField.text;
        GameManager.Instance.SavePlayerData();
        GameManager.Instance.LoadPlayerData();

        // Check First Play
        if (GameManager.Instance.CheckPlayProgress("FirstStart") == true)
        {
            if(DialogueControl_HomeScene != null)
            {
                DialogueControl_HomeScene.StartFirstDialogue();
            }
        }
    }

}
