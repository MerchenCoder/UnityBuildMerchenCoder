using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueBtn : MonoBehaviour
{

    //플레이어 위치 저장을 위한 변수//
    Button dialogueBubbleBtn;
    GameObject player;
    GameManager.PlayerData.playerPosition nowPlayerPosition;


    Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBubbleBtn = GetComponent<Button>();
        dialogueBubbleBtn.interactable = false;
        player = GameObject.FindWithTag("Player");

        dialogueBubbleBtn.onClick.RemoveAllListeners();
        dialogueBubbleBtn.onClick.AddListener(SavePlayerPosition);

        dialogue = GetComponent<Dialogue>();
    }

    public void DialogueBtnDown()
    {
        dialogue.DialogueStart();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Player"))
        {
            dialogueBubbleBtn.interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals("Player"))
        {
            dialogueBubbleBtn.interactable = false;
        }
    }

    private void SavePlayerPosition()
    {
        string thisSceneName = SceneManager.GetActiveScene().name;
        string chapter = thisSceneName.Substring(0, 1);
        GameManager.Instance.playerData.chapterCurrentScene[int.Parse(chapter) - 1] = thisSceneName;

        //포지션 기록

        nowPlayerPosition.x = player.transform.localPosition.x;
        nowPlayerPosition.y = player.transform.localPosition.y;
        nowPlayerPosition.z = player.transform.localPosition.z;
        GameManager.Instance.playerData.playLog[thisSceneName] = nowPlayerPosition;
        GameManager.Instance.SavePlayerData();
    }


}
