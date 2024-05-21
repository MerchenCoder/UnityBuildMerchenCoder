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
            }

        }
        else
        {
            Debug.Log("해당 변수가 이미 존재합니다"); // 추후 UI로 변경
        }
    }
}
