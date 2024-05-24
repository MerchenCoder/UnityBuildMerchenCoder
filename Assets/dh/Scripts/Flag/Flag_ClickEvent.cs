using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Flag_ClickEvent : MonoBehaviour, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        if (FlagManager.instance != null)
        {
            Debug.Log(gameObject.name);
            if (FlagManager.instance.flagStr == gameObject.name)
            {
                FlagManager.instance.OffFlag();
            }
        }
    }
}
