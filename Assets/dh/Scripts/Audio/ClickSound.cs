using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSound : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool isNode;
    [Header("node")]
    [SerializeField] private NodeNameManager nodeNameManager;
    [Header("not node")]
    [SerializeField] private AutoAudioSetting autoAudioSetting;
    [SerializeField] private int index;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isNode)
        {
            nodeNameManager.NodeComponentClickSound();
        }
        else
        {
            autoAudioSetting.OnClickSound_Index(index);
        }
    }
}
