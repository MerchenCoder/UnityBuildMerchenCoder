using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EditAfterSubmit : MonoBehaviour
{
    [SerializeField] private GameObject resultCanvas;
    [SerializeField] private Button stopButton;
    private void Start()
    {
        resultCanvas = NodeManager.Instance.resultCanvas;
        stopButton = resultCanvas.GetComponentInChildren<Button>();
    }
    public void RefreshResultCanvas()
    {
        if (stopButton != null)
            stopButton.onClick.Invoke();
    }
}
