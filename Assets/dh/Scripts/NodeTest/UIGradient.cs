using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGradient : MonoBehaviour
{
    public Material gradientMat;
    public Color topColor;
    public Color bottomColor;
    // Start is called before the first frame update
    void Start()
    {
        gradientMat.SetColor("_Color", topColor);
        gradientMat.SetColor("_Color2", bottomColor);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
