using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataErrorManager : MonoBehaviour
{
    private bool errorFlag;
    public bool ErrorFlag
    {
        get
        {
            return errorFlag;
        }
        set
        {
            errorFlag = value;
        }
    }
}
