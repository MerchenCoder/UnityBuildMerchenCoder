using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterManager : MonoBehaviour
{
    public List<GameObject> chapterBooks;

    // Start is called before the first frame update
    void Start()
    {
        DataManager.Instance.LoadGameData();
        ChapterSetting();
    }

    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveGameData();
    }

    public void ChapterUnlock(int chapterNum)
    {

    }


    public void ChapterSetting()
    {
        for (int i = 0; i < DataManager.Instance.data.chapterIsUnlock.Length; i++)
        {
            if (DataManager.Instance.data.chapterIsUnlock[i] == true)
            {
                chapterBooks[i].GetComponent<Button>().interactable = true;//버튼 활성화
            }
            else
            {
                // chapterBooks[i].GetComponent<Image>().color = Color.black;
                chapterBooks[i].GetComponent<Button>().interactable = false; //버튼 비활성화
            }
        }

    }


}
