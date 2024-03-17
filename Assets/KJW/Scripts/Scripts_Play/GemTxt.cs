using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class GemTxt : MonoBehaviour
{
    Text gemNumText;

    // Start is called before the first frame update
    void Start()
    {
        gemNumText = GetComponent<Text>();
        // GameManager의 젬 상태 변경 이벤트에 대한 리스너 등록
        GameManager.Instance.OnGemChanged += UpdateGemDisplay;
        // 초기화면에 젬 상태를 업데이트
        UpdateGemDisplay(GameManager.Instance.GetNowGem());
    }

    // 젬 상태가 변경될 때마다 호출되는 함수
    private void UpdateGemDisplay(int gemNum)
    {
        gemNumText.text = gemNum.ToString();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGemChanged -= UpdateGemDisplay;
    }
}
