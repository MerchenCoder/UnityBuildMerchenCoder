using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Input : MonoBehaviour
{
    private void OnEnable() {
        this.GetComponent<TMP_InputField>().text = string.Empty;
    }
}
