using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleGroup2Manager : MonoBehaviour
{
    private ToggleGroup toggleGroup;
    public Button button;
    // Start is called before the first frame update

    // private Toggle

    void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
        //button.interactable = false;
    }
    public void OnToggleValueChagned(bool isOn)
    {
        Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
        int selectedToggleCount = 1;
        foreach (Toggle toggle in toggles)
        {
            if (toggle.isOn)
            {
                break; // 하나라도 선택되었다면 반복문 종료
            }
            else
            {
                selectedToggleCount++;

            }

        }

        // 선택된 Toggle이 있으면 버튼 활성화, 그렇지 않으면 비활성화
        button.interactable = selectedToggleCount >= 1 && selectedToggleCount <= 2;
        button.GetComponent<MakeParaNodeInstanceBtn>().selectType = selectedToggleCount;
        Debug.Log("Type: " + button.GetComponent<MakeParaNodeInstanceBtn>().selectType);

    }

    //close button 눌렀을 때 모달 창 reset
    public void ResetToggleSelection()
    {
        Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
        // toggleGroup.SetAllTogglesOff();
        //SetAllTogglesOff()가 동작을 안해서 수동으로 off... 이유는 모름...
        // foreach (Toggle toggle in toggles)
        // {
        //     if (toggle.isOn)
        //     {
        //         toggle.isOn = false;
        //     }
        // }
        for (int i = 1; i < toggles.Length; i++)
        {
            if (toggles[i].isOn) toggles[i].isOn = false;
        }
        //button.interactable = false;

    }
}
