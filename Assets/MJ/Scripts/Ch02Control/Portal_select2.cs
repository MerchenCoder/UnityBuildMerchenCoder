using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_select2 : MonoBehaviour
{
    [SerializeField] string missionCompleteScene;
    [SerializeField] string missionIncompleteScene;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance.CheckPlayProgress("Mission2-15Y")
            || GameManager.Instance.CheckPlayProgress("Chap2Ending"))
        {
            SceneChange.Instance.ChangeToThisScene(missionCompleteScene);
        }
        else
        {
            SceneChange.Instance.ChangeToThisScene(missionIncompleteScene);
        }
    }

    public void RequestMoveScene()
    {
        if (GameManager.Instance.CheckPlayProgress("Mission2-15Y")
            || GameManager.Instance.CheckPlayProgress("Chap2Ending"))
        {
            SceneChange.Instance.ChangeToThisScene(missionCompleteScene);
        }
        else
        {
            SceneChange.Instance.ChangeToThisScene(missionIncompleteScene);
        }
    }
}