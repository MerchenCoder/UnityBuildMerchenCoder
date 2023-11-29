using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizActive : MonoBehaviour
{
    public GameObject quizInfoPanel;

    public void QuizActiveTrue()
    {
        quizInfoPanel.SetActive(true);
    }
}
