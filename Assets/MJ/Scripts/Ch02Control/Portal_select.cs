using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_select : MonoBehaviour
{
    [SerializeField] string missionCompleteScene;
    [SerializeField] string missionIncompleteScene;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance.CheckPlayProgress("Mission2-13Y")
            || GameManager.Instance.CheckPlayProgress("Mission2-14N")
            || GameManager.Instance.CheckPlayProgress("Mission2-14Y")
            || GameManager.Instance.CheckPlayProgress("Mission2-15N")
            || GameManager.Instance.CheckPlayProgress("Mission2-15Y")
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
        if (GameManager.Instance.CheckPlayProgress("Mission2-13Y")
            || GameManager.Instance.CheckPlayProgress("Mission2-14N")
            || GameManager.Instance.CheckPlayProgress("Mission2-14Y")
            || GameManager.Instance.CheckPlayProgress("Mission2-15N")
            || GameManager.Instance.CheckPlayProgress("Mission2-15Y")
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