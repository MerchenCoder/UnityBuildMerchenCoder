using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MaskSizeControl : MonoBehaviour
{
    public Image imageToResize;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null && imageToResize != null)
        {
            Vector2 size = rectTransform.rect.size;
            this.GetComponent<RectTransform>().sizeDelta = size;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
