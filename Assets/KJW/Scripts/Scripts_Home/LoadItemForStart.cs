using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadItemForStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetActiveAfter1s", 0.5f);
    }
    
    public void SetActiveAfter1s()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
