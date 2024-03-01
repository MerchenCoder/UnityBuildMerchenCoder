using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshLayout : MonoBehaviour
{
    public GameObject layout1;
    public GameObject layout2;

    private void OnEnable()
    {
        Debug.Log("활성화");
        Refresh();
    }
    public void Refresh()
    {
        Debug.Log("refresh");
        // LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)layout2.transform); //content sizefitter 버그 해결을 위한 리프레시
        // LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)layout1.transform); //content sizefitter 버그 해결을 위한 리프레시
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)this.transform); //content sizefitter 버그 해결을 위한 리프레시

    }
}
