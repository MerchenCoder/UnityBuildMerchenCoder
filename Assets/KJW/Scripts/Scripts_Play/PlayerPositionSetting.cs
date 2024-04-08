using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerPositionSetting : MonoBehaviour
{
    GameManager.PlayerData playerData;
    GameManager.PlayerData.playerPosition nowPlayerPosition;
    string thisSceneName;

    void Start()
    {

        nowPlayerPosition = new GameManager.PlayerData.playerPosition();
        thisSceneName = SceneManager.GetActiveScene().name;

        // 이전 씬에 따른 플레이어 위치 조정
        if (SceneChange.Instance.beforeScene == "1_2_town" && thisSceneName == "1_1_farmer") { this.transform.localPosition = new Vector3(48.1f, -1.015f, 0); this.GetComponent<SpriteRenderer>().flipX = false; }
        else if (SceneChange.Instance.beforeScene == "1_3_castle" && thisSceneName == "1_2_town") { this.transform.localPosition = new Vector3(48.6f, -1.015f, 0); this.GetComponent<SpriteRenderer>().flipX = false; }
        else if (SceneChange.Instance.beforeScene == "1_4_forest" && thisSceneName == "1_3_castle") { this.transform.localPosition = new Vector3(26.84f, -1.015f, 0); this.GetComponent<SpriteRenderer>().flipX = false; }
        else if (SceneChange.Instance.beforeScene == "1_1_farmer" && thisSceneName == "1_2_town") { this.transform.localPosition = new Vector3(-7.86f, -1.015f, 0); this.GetComponent<SpriteRenderer>().flipX = true; }
        else if (SceneChange.Instance.beforeScene == "1_2_town" && thisSceneName == "1_3_castle") { this.transform.localPosition = new Vector3(-8.97f, -0.92f, 0); this.GetComponent<SpriteRenderer>().flipX = true; }
        else if (SceneChange.Instance.beforeScene == "1_3_castle" && thisSceneName == "1_4_forest") { this.transform.localPosition = new Vector3(-8.24f, -1.015f, 0); this.GetComponent<SpriteRenderer>().flipX = true; }
        else
        {
            GameManager.Instance.LoadPlayerData();
            playerData = GameManager.Instance.playerData;
            this.transform.localPosition = new Vector3(playerData.playLog[thisSceneName].x, playerData.playLog[thisSceneName].y, playerData.playLog[thisSceneName].z);
        }
    }
    private void OnDisable()
    {
        //마지막 플레이 씬 기록
        string chapter = thisSceneName.Substring(0, 1);
        if (GameManager.Instance != null)
            GameManager.Instance.playerData.chapterCurrentScene[int.Parse(chapter) - 1] = thisSceneName;

        //포지션 기록
        nowPlayerPosition.x = transform.localPosition.x;
        nowPlayerPosition.y = transform.localPosition.y;
        nowPlayerPosition.z = transform.localPosition.z;

        GameManager.Instance.playerData.playLog[thisSceneName] = nowPlayerPosition;
        GameManager.Instance.SavePlayerData();
    }

}


