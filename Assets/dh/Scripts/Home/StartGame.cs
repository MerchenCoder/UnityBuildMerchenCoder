using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour, IPointerClickHandler
{
    private GameObject startInfoText;
    private GameObject loading;
    // Start is called before the first frame update
    void Start()
    {
        startInfoText = transform.GetChild(0).gameObject;
        loading = transform.GetChild(1).gameObject;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("패널 클릭");
        startInfoText.SetActive(false);
        loading.SetActive(true);
        StartCoroutine(loading.GetComponent<GameLoadingScript>().Loading());

    }
}
