using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionManager : MonoBehaviour
{
    private string missionTitle;
    private string missionInfo;
    // Start is called before the first frame update

    GameObject title;
    GameObject rewards;
    GameObject info;
    void Start()
    {

        title = transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        rewards = transform.GetChild(1).GetChild(0).GetChild(3).gameObject;
        info = transform.GetChild(1).GetComponentInChildren<NodeLabelControl>().transform.parent.GetChild(0).gameObject;

        missionTitle = "문제 " + NodeGameManager.Instance.missionData.missionCode + " : " + NodeGameManager.Instance.missionData.missionTitle;
        missionInfo = NodeGameManager.Instance.missionData.missionInfo;

        title.GetComponent<TextMeshProUGUI>().text = missionTitle;
        rewards.transform.GetChild(2).GetComponent<Text>().text = NodeGameManager.Instance.missionData.reward.ToString();
        info.GetComponentInChildren<TextMeshProUGUI>().text = missionInfo;

        Debug.Log("미션 정보 설정 완료");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
