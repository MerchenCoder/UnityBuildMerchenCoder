using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RefreshCanvas : MonoBehaviour
{
    public GameObject player;
    public Vector3 originVecor3;

    private void Start()
    {
        if (player != null) originVecor3 = player.transform.localPosition;
    }

    private void LoadOriginPosition()
    {
        if (player != null)
        {
            player.transform.localPosition = originVecor3;
        }
    }

    public void stopPlaying()
    {
        MonoBehaviour[] allScripts = Object.FindObjectsOfType<MonoBehaviour>();

        foreach (MonoBehaviour script in allScripts)
        {
            script.StopAllCoroutines();
        }
        LoadOriginPosition();
        this.gameObject.SetActive(false);
    }
}