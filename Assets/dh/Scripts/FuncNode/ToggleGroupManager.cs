using UnityEngine;
using UnityEngine.UI;

public class FunctionToggleGroupManager : MonoBehaviour
{
    private ToggleGroup toggleGroup;
    public Button button;
    // Start is called before the first frame update

    // private Toggle

    void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
        button.interactable = false;
    }
    public void OnToggleValueChagned(bool isOn)
    {
        Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
        int selectedToggleCount = 0;
        foreach (Toggle toggle in toggles)
        {
            selectedToggleCount++;
            if (toggle.isOn)
            {
                break; // 하나라도 선택되었다면 반복문 종료
            }
        }

        // 선택된 Toggle이 있으면 버튼 활성화, 그렇지 않으면 비활성화
        button.interactable = selectedToggleCount > 0;
        button.GetComponent<FunctionMaker>().selectType = selectedToggleCount;
        Debug.Log("Type: "+button.GetComponent<FunctionMaker>().selectType);
    }

    //close button 눌렀을 때 모달 창 reset
    public void ResetToggleSelection()
    {

        // toggleGroup.setActive(true);
        toggleGroup.SetAllTogglesOff();
        button.interactable = false;

    }

}
