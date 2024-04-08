using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterManager : MonoBehaviour
{
    public List<GameObject> chapterBooks;

    // Start is called before the first frame update
    void Awake()
    {
        // DataManager.Instance.LoadGameData();
        ChapterSetting();
    }
    public void ChapterUnlock(int chapterNum)
    {

    }


    public void ChapterSetting()
    {
        for (int i = 0; i < DataManager.Instance.gameStateData.chapterIsUnlock.Length; i++)
        {
            if (DataManager.Instance.gameStateData.chapterIsUnlock[i] == true)
            {
                chapterBooks[i].GetComponent<Button>().interactable = true;//버튼 활성화
                chapterBooks[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                // chapterBooks[i].GetComponent<Image>().color = Color.black;
                chapterBooks[i].GetComponent<Button>().interactable = false; //버튼 비활성화
                chapterBooks[i].transform.GetChild(0).gameObject.SetActive(true);

            }
        }
    }


    public void SetChapterCurrentMission(int chapterNum)
    {
        string currentMission = GameManager.Instance.playerData.chapterCurrentMission[chapterNum - 1];
        if (currentMission != "" && currentMission != null)
        {
            Debug.Log($"진행 중인 미션 코드 {currentMission} 불러오기");
            GameManager.Instance.LoadMissionData(currentMission);

        }
    }
}
