using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RefreshCanvas : MonoBehaviour
{
    public GameObject player;
    public Vector3 originVecor3;

    public GameObject playerChatBubble;
    private void Start()
    {
        if (player != null) originVecor3 = player.transform.localPosition;
    }

    private void LoadOriginPosition()
    {
        if (player != null)
        {
            player.transform.localPosition = originVecor3;
        }
    }

    public void stopPlaying()
    {
        MonoBehaviour[] allScripts = Object.FindObjectsOfType<MonoBehaviour>();

        foreach (MonoBehaviour script in allScripts)
        {
            script.StopAllCoroutines();
        }
        LoadOriginPosition();
        playerChatBubble.GetComponentInChildren<TextMeshProUGUI>().text = null;
        Debug.Log("말풍선 초기화, 비활성화");
        playerChatBubble.SetActive(false);

        //에러 메시지 박스 초기화
        GetComponent<RunErrorMsg>().InActiveErrorMsg();

        this.gameObject.SetActive(false);
    }
}