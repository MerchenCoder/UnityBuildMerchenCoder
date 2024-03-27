using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaEndInteraction : MonoBehaviour
{
    [Header("Quiz")]
    public bool isConnectedWithQuiz;
    public QuizActive quizActive;

    [Header("Flow")]
    public bool isConnectedWithNextFlow;
    public GameObject nextFlow;

    [Header("Save")]
    public bool isConnectedWithSave;
    public string playPoint;


    public void EndDialogueInteraction()
    {
        if (isConnectedWithQuiz)
        {
            if(quizActive != null)
            {
                quizActive.gameObject.SetActive(true);
                //quizActive.QuizActiveTrue();
            }
        }
        if (isConnectedWithNextFlow)
        {
            if (nextFlow != null)
            {
                nextFlow.SetActive(true);
            }
        }
        if (isConnectedWithSave)
        {
            GameManager.Instance.SavePlayProgress(playPoint, true);
        }
    }
}
