using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel1FuncSetting : MonoBehaviour
{
        //toggle reset

        private ToggleGroupManager toggleGroupManager;

        private void OnDisable() {
            toggleGroupManager = this.GetComponentInChildren<ToggleGroupManager>();
            toggleGroupManager.ResetToggleSelection();
        }
    
}
