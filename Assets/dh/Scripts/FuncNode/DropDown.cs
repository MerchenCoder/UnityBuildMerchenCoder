using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DropDown : MonoBehaviour
{
    private void OnEnable() {
        this.GetComponent<TMP_Dropdown>().value = -1;
    }
}
