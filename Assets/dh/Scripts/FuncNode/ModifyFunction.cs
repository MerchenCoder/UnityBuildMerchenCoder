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
        foreach (GameObject functionCanvas in FunctionManager.Instance.myfuncCanvas)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = functionCanvas.name.Split("_")[0].ToString();
            funcListDropDown.options.Add(option);

        }
        funcListDropDown.value = -1;
    }


    public void FuncCanvasActive()
    {
        FunctionManager.Instance.myfuncCanvas[funcListDropDown.value].gameObject.SetActive(true);
        modifyCanvas.gameObject.SetActive(false);
    }

    public void RemoveFunc()
    {
        int selectedIndex = funcListDropDown.value;

        if (selectedIndex >= 0 && selectedIndex < FunctionManager.Instance.myfuncCanvas.Count)
        {
            GameObject removeFuncCanvas = FunctionManager.Instance.myfuncCanvas[funcListDropDown.value];
            FunctionManager.Instance.myfuncCanvas.RemoveAt(funcListDropDown.value);
            FunctionManager.Instance.myfuncNodes.RemoveAt(funcListDropDown.value);
            Debug.Log(removeFuncCanvas.name.ToString() + " 함수가 삭제되었습니다.");
            Destroy(removeFuncCanvas.gameObject);
            GameObject functionManager = GameObject.Find("FunctionManager").gameObject;
            Destroy(functionManager.transform.GetChild(funcListDropDown.value));
            modifyCanvas.gameObject.SetActive(false);
        }

    }

}
