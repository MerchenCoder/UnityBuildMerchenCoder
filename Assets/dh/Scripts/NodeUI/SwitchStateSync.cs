using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchStateSync : MonoBehaviour
{
    // Start is called before the first frame update
    SwitchSlide_Delete switchSlider;
    private void Start()
    {
        switchSlider = GetComponent<SwitchSlide_Delete>();
        NodeManager.Instance.DeleteModeChanged += OnDeleteModeChanged;
    }

    private void OnDeleteModeChanged(bool newState)
    {
        //Debug.Log("switchSlider.isoff = " + switchSlider.isoff.ToString());
        if (switchSlider.isoff == newState)
        {
            if (newState)
            {
                switchSlider.SwitchToOn();
            }
            else
            {
                switchSlider.SwitchToOff();
            }
        }

    }

}
