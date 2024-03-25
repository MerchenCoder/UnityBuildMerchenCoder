using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayProgressControl : MonoBehaviour
{
    public string activePlayPoint;

    void Start()
    {
        if (!GameManager.Instance.CheckPlayProgress(activePlayPoint))
        {
            gameObject.SetActive(false);
            if (TryGetComponent<QuizActive>(out QuizActive qa))
            {
                qa.AfterMissionDialogue.SetActive(false);
            }
        }
    }

}
