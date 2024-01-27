using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel2FuncSetting : MonoBehaviour
{
    private void OnDisable() {
        //Panel1 끄기
        transform.parent.GetChild(0).gameObject.SetActive(false);
    }
}
