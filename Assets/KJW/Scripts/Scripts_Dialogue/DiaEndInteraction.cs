using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaEndInteraction : MonoBehaviour
{
    public bool isConnectedWithQuiz;
    public QuizActive quizActive;

    public void EndDialogueInteraction()
    {
        if (isConnectedWithQuiz)
        {
            if(quizActive != null)
            {
                quizActive.gameObject.SetActive(true);
                quizActive.QuizActiveTrue();
            }
        }
    }
}
