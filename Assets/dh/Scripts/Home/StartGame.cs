using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour, IPointerClickHandler
{
    private GameObject startInfoText;
    public GameObject loading;
    public GameObject startButtons;
    // Start is called before the first frame update
    void Start()
    {
        startInfoText = transform.GetChild(0).gameObject;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("패널 클릭");
        startInfoText.SetActive(false);
        startButtons.SetActive(true);


    }
    public void GameLoading()
    {
        startButtons.SetActive(false);
        loading.SetActive(true);
        StartCoroutine(loading.GetComponent<GameLoadingScript>().Loading());

    }
}
