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
    public GameObject testButton;
    public GameObject copyrightButton;
    public AudioControl audioControl;
    // Start is called before the first frame update
    void Start()
    {
        startInfoText = transform.GetChild(0).gameObject;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (startInfoText.activeSelf == true)
        {
            Debug.Log("패널 클릭");
            audioControl.SoundPlayOneShot(0);
            startInfoText.SetActive(false);

            if (PlayerPrefs.HasKey("autoLogin") && PlayerPrefs.GetInt("autoLogin") == 1)
            {
                print("자동 로그인합니다.");
                GetComponent<Sign>().Login(PlayerPrefs.GetString("userId"), PlayerPrefs.GetString("userPwd"));

            }
            else
            {
                startButtons.SetActive(true);
                copyrightButton.SetActive(true);
                testButton.SetActive(false);
            }
        }




    }
    public void GameLoading()
    {
        startButtons.SetActive(false);
        loading.SetActive(true);
        StartCoroutine(loading.GetComponent<GameLoadingScript>().Loading());

    }
}
