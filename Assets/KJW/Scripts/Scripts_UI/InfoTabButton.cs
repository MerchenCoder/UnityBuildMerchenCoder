using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTabButton : MonoBehaviour
{
    public InfoPagesController pagesController;

    public void TabBtnOnClick()
    {
        pagesController.InfoTabButtonOnClick(this.gameObject.name);
    }
}
