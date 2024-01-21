using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ParameterSetting : MonoBehaviour
{
    private bool isOn=true;
    public bool IsOn {
        get {
            return isOn;
        }
        set {
            isOn = value;
        }
    }
    private TextMeshProUGUI label;
    private TMP_InputField input;
    private TMP_Dropdown dropdown;


    [SerializeField] Sprite[] swapImage = new Sprite[2];

    // Start is called before the first frame update
    void Start()
    {
        label = transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>();
        dropdown = transform.parent.parent.GetChild(1).GetComponent<TMP_Dropdown>();
        input = transform.parent.parent.GetChild(2).GetComponent<TMP_InputField>();
        ResetButton();

        this.GetComponent<Button>().onClick.AddListener(()=>{
            if(isOn==false){ //미선택 상태에서 선택으로 변화
                this.isOn = true;
                GetComponent<Image>().sprite = swapImage[1];
                label.color = Color.black;
                dropdown.interactable = true;
                input.interactable = true;


            }
            else {
                this.isOn = false;
                GetComponent<Image>().sprite = swapImage[0];
                label.color = Color.gray;
                dropdown.interactable = false;
                input.interactable = false;

            }
        });
    }

    private void OnEnable() {
        label = transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>();
        dropdown = transform.parent.parent.GetChild(1).GetComponent<TMP_Dropdown>();
        input = transform.parent.parent.GetChild(2).GetComponent<TMP_InputField>();
        ResetButton();
    }
    public void ResetButton(){
        this.isOn = true;
        this.GetComponent<Image>().sprite = swapImage[1];
        label.color = Color.black;
        dropdown.interactable = true;
        input.interactable = true;

    }
}
