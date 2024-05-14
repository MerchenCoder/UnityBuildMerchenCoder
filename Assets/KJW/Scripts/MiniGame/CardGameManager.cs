using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGameManager : MonoBehaviour
{
    public string nextPlayPoint;
    public CardContainer cardContainer;
    public Image mainPanel;
    public GameObject failedPanel;
    public GameObject clearPanel;
    public SliderBar sliderBar;
    public GameObject cardHolder;
    public GameObject afterMiniGameDia;
    public int reward;
    // 카드 담을 List
    List<Transform> children;

    // 사운드
    public AudioSource startSound;

    private void Start()
    { 
        Init();
        
        // 부모 GameObject에서 모든 자식 GameObject 가져오기
        children = new List<Transform>();
        foreach (Transform child in cardHolder.transform)
        {
            children.Add(child);
        }

        GameStart();
    }

    private void OnEnable()
    {
        Init();
        GameStart();
    }

    public void Init()
    {
        failedPanel.SetActive(false);
        clearPanel.SetActive(false);
        // 클릭 막기
        cardContainer.canSelect = false;
        // 슬라이더 초기화
        sliderBar.ResetSlider();
        cardContainer.Init();
    }

    public void GameStart()
    {
        startSound.Play();
        // 카드 섞기
        StartCoroutine(ShuffleCard());
    }

    public void GameOver()
    {
        // 게임오버 패널 활성화
        failedPanel.SetActive(true);
    }

    public void Restart()
    {
        // 초기화
        Init();
        // 다시 시작
        GameStart();
    }

    private void ShuffleCards()
    {
        // 자식 GameObject들을 무작위로 재배치
        for (int i = 0; i < children.Count; i++)
        {
            int randomIndex = Random.Range(i, children.Count);
            Transform temp = children[i];
            children[i] = children[randomIndex];
            children[randomIndex] = temp;
        }

        // 자식 GameObject들의 순서를 변경
        foreach (Transform child in children)
        {
            child.SetAsLastSibling();
        }
    }

    IEnumerator ShuffleCard()
    {
        // 카드 재배치
        ShuffleCards();
        // 카드 보여주기 (3초)
        foreach (Transform child in children)
        {
            child.GetComponent<Animator>().SetTrigger("ShowCard");
        }
        yield return new WaitForSeconds(3f);
        // 카드 뒤집기 (1초)
        foreach (Transform child in children)
        {
            child.GetComponent<Animator>().SetTrigger("HideCard");
        }
        yield return new WaitForSeconds(1f);
        // 게임 스타트
        sliderBar.GameStart();
        cardContainer.canSelect = true;
        yield return null;
    }


    public void GameClear()
    {
        clearPanel.SetActive(true);
        if (afterMiniGameDia != null) afterMiniGameDia.SetActive(true);
        sliderBar.isStarted = false;
        //보상 반영
        GameManager.Instance.GetSomeGem(reward);
        // 세이브
        if(nextPlayPoint != "")
        {
            GameManager.Instance.SavePlayProgress(nextPlayPoint, true);
        }
    }

}
