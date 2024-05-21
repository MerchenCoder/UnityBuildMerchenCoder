using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentUser : MonoBehaviour
{
    TMP_Text userId_text;
    [SerializeField] string format = "계정 ID : ";
    [SerializeField] string currentUser;
    // Start is called before the first frame update
    void Start()
    {
        userId_text = GetComponent<TMP_Text>();

        if (PlayerPrefs.HasKey("userId"))
            currentUser = PlayerPrefs.GetString("userId");

        if (string.IsNullOrEmpty(currentUser))
        {
            return;
        }
        else
        {
            userId_text.text = format + currentUser;
        }
    }


}
