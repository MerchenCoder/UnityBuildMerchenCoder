using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetGemControl : MonoBehaviour
{
    Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(GetGems);
    }
    void GetGems()
    {
        GameManager.Instance.GetSomeGem(1000);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
