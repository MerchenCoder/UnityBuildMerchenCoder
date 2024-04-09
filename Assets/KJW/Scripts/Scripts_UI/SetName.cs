using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetName : MonoBehaviour
{
    public InputField inputField;
    public Text infoText;
    public DialogueControl_HomeScene DialogueControl_HomeScene;

    bool firstClick = true;

    public void SetButtonDown()
    {
        if(inputField.text == "")
        {
            if (firstClick)
            {
                infoText.text += "이름을 입력 후 확인 버튼을 눌러주세요.";
                firstClick = false;
            }
        }
        else
        {
            GameManager.Instance.playerData.name = inputField.text;
            PlayerPrefs.SetString("player_name", inputField.text);
            GameManager.Instance.SavePlayerData();

            // Check First Play
            if (GameManager.Instance.CheckPlayProgress("FirstStart") == true)
            {
                if (DialogueControl_HomeScene != null)
                {
                    DialogueControl_HomeScene.StartFirstDialogue();
                }
            }
        }
    }

}
