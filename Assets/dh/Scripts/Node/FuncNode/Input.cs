using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Input : MonoBehaviour
{
    internal static int touchCount;
    internal static Vector3 mousePosition;

    internal static Touch GetTouch(int v)
    {
        throw new NotImplementedException();
    }

    private void OnEnable() {
        this.GetComponent<TMP_InputField>().text = string.Empty;
    }
}
