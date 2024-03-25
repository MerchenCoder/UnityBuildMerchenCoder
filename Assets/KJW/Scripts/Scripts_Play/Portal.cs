using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] string NextSceneStr;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneChange.Instance.ChangeToThisScene(NextSceneStr);
    }

    public void RequestMoveScene()
    {
        SceneChange.Instance.ChangeToThisScene(NextSceneStr);
    }
}
