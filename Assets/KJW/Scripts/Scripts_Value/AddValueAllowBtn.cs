using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddValueAllowBtn : MonoBehaviour
{
    public ValueManager valueManager;
    public TMP_InputField inputField;
    private int type;

    [SerializeField] private GameObject errorTMP;
    private string originErrorMsg;



    [SerializeField] private GameObject blackBGPanel;
    [SerializeField] private GameObject valueNameSettingUI;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        // if (valueManager == null)
        //     //valueManager = GameObject.Find("ValueManager").gameObject.GetComponent<ValueManager>();
        //     valueManager = GetComponentInParent<Canvas>().GetComponentInChildren<ValueManager>(true);

        if (GetComponentInParent<Canvas>(true).name == "Canvas_UI")
        {
            Debug.Log("a;lfjasfl;saf;lj");
            valueManager = GameObject.Find("MainCanvas").GetComponentInChildren<ValueManager>(true);
        }
        else
        {
            valueManager = GetComponentInParent<Canvas>().GetComponentInChildren<ValueManager>(true);
        }

        button = GetComponent<Button>();

        button.onClick.AddListener(AddValue);

        errorTMP.SetActive(false);
    }

    public void AddValue()
    {
        if (transform.parent.CompareTag("data_int"))
        {
            type = 0;
        }
        else if (transform.parent.CompareTag("data_bool"))
        {
            type = 1;
        }
        else if (transform.parent.CompareTag("data_string"))
        {
            type = 2;
        }

        if (!valueManager.isExistValue(inputField.text.Trim()))
        {
            //0603 암구호 변수 튜토를 위해 오류 메세지 설정
            if (FlagManager.instance != null)
            {
                if (FlagManager.instance.flagStr.Equals("SetValuableName_암구호") && !inputField.text.Trim().Equals("암구호"))
                {
                    originErrorMsg = errorTMP.GetComponent<TMP_Text>().text;
                    errorTMP.GetComponent<TMP_Text>().text = "변수 이름을 '암구호'로 해주세요.";
                    errorTMP.SetActive(true);
                    return;
                }
            }

            valueManager.AddValue(type, inputField.text.Trim());

            // 혹시 변수 블록 생성되면 쓸 코드
            ////addedValue = Instantiate(valuePrefab, transform.parent);
            ////addedValue.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = inputField.text;
            ///

            // 튜토리얼 플래그 추가 240522
            if (FlagManager.instance != null)
            {
                if (FlagManager.instance.flagStr == "SetValuableName")
                {
                    FlagManager.instance.OffFlag();
                }
                //0603 암구호 변수 플래그 추가
                else if (FlagManager.instance.flagStr.Equals("SetValuableName_암구호"))
                {
                    FlagManager.instance.OffFlag();
                }

            }
            errorTMP.GetComponent<TMP_Text>().text = originErrorMsg;
            errorTMP.SetActive(false);
            blackBGPanel.SetActive(false);
            valueNameSettingUI.SetActive(false);

        }
        else
        {
            if (errorTMP.GetComponent<TMP_Text>().text.StartsWith("변수 이름을"))
            {
                errorTMP.GetComponent<TMP_Text>().text = originErrorMsg;

            }
            errorTMP.SetActive(true);

            Debug.Log("해당 변수가 이미 존재합니다"); // 추후 UI로 변경

        }
    }
}
