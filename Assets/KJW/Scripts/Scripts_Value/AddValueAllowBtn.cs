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
        if(valueManager == null)
            valueManager = GameObject.Find("ValueManager").gameObject.GetComponent<ValueManager>();
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
        }
        else
        {
            Debug.Log("해당 변수가 이미 존재합니다"); // 추후 UI로 변경
        }
    }
}
