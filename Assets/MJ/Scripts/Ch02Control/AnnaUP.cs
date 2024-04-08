using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnnaUP : MonoBehaviour
{
    public QuizActive quizActive;
    public bool quizSolve;

    void Update()
    {
        quizSolve = quizActive.isDone;
        if (quizSolve == false)
        {
            gameObject.SetActive(false);
            Debug.Log(quizSolve);
        }
        else
        {
            gameObject.SetActive(true);
            Debug.Log(quizSolve);
        }
    }
}