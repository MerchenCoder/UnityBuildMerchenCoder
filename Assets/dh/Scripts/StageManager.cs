using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public List<GameObject> stages;

    // Start is called before the first frame update
    void Start()
    {
        DataManager.Instance.LoadGameData();
        StageSetting();
    }

    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveGameData();
    }

    public void StageUnlock(int stageNum)
    {

    }


    public void StageSetting()
    {
        for (int i = 0; i < DataManager.Instance.data.ch1StageIsUnlock.Length; i++)
        {
            if (DataManager.Instance.data.ch1StageIsUnlock[i] == true)
            {
                stages[i].transform.GetChild(0).GetComponent<Image>().color = Color.white;
                stages[i].GetComponent<Button>().interactable = true;//버튼 활성화
            }
            else
            {
                stages[i].transform.GetChild(0).GetComponent<Image>().color = Color.black;
                stages[i].GetComponent<Button>().interactable = false; //버튼 비활성화
            }
        }

    }
}