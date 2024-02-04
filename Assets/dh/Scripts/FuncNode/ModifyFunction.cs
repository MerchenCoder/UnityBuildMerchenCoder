using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ModifyFunction : MonoBehaviour
{

    Button btn;
    public Canvas modifyCanvas;
    public TMP_Dropdown funcListDropDown;

    // Start is called before the first frame update
    void Start()
    {
        btn = this.GetComponent<Button>();

        btn.onClick.AddListener(ModifyFunc);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void ModifyFunc()
    {
        if (FunctionManager.Instance.totalFunction != 0)
        {
            SetDropdownOpts();
            modifyCanvas.gameObject.SetActive(true);
        }
    }

    void SetDropdownOpts()
    {
        funcListDropDown.options.Clear();
        foreach (Canvas functionCanvas in FunctionManager.Instance.functionCanvas)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = functionCanvas.name.Split("_")[0].ToString();
            funcListDropDown.options.Add(option);

        }
        funcListDropDown.value = -1;
    }


    public void FuncCanvasActive()
    {
        FunctionManager.Instance.functionCanvas[funcListDropDown.value].gameObject.SetActive(true);
        modifyCanvas.gameObject.SetActive(false);
    }

    public void RemoveFunc()
    {
        int selectedIndex = funcListDropDown.value;

        if (selectedIndex >= 0 && selectedIndex < FunctionManager.Instance.functionCanvas.Count)
        {
            Canvas removeFuncCanvas = FunctionManager.Instance.functionCanvas[funcListDropDown.value];
            FunctionManager.Instance.functionCanvas.RemoveAt(funcListDropDown.value);
            Debug.Log(removeFuncCanvas.name.ToString() + " 함수가 삭제되었습니다.");
            Destroy(removeFuncCanvas.gameObject);
            modifyCanvas.gameObject.SetActive(false);
        }

    }

}