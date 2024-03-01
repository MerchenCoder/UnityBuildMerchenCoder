using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshLayout : MonoBehaviour
{

    public void RebuildLayout()
    {
        StartCoroutine(nameof(CorRebuildLayout), (RectTransform)transform);
    }

    IEnumerator CorRebuildLayout(RectTransform obj)
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(obj);

    }
}
