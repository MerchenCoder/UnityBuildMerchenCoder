using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavasFuncSetting : MonoBehaviour
{
    private void OnEnable() {
        //Panel1 켜기
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnDisable() {

        //1.Panel2 끄기
        transform.GetChild(1).gameObject.SetActive(false);
        //2.Panel1 끄기
        transform.GetChild(0).gameObject.SetActive(false);        
    }
}
