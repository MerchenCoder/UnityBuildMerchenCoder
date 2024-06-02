using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloseButton : MonoBehaviour
{
    public GameObject Panel;
    public delegate void CloseEventHandler(bool isClose); //델리게이트 0603
    public CloseEventHandler OnClose; //델리게이트 인스턴스

    public void Close()
    {
        Panel.SetActive(false);
        this.gameObject.SetActive(false);

        OnClose?.Invoke(true);
    }
}
