using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void ChangetoHome()
    {
        SceneManager.LoadScene("Home");
    }
    public void ChangetoChapter()
    {
        SceneManager.LoadScene("Chapter");
    }

}
