using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardContainer : MonoBehaviour
{
    public int totalPairs = 9; // 전체 카드 쌍 수
    private int matchedPairs = 0; // 맞춘 카드 쌍 수

    public Card firstCard; // 첫 번째 선택한 카드
    public Card secondCard; // 두 번째 선택한 카드

    public bool canSelect; // 카드 선택 가능 여부
    private CardGameManager CardGameManager;

    public AudioSource audioSource;

    void Start()
    {
        CardGameManager = transform.parent.parent.GetComponent<CardGameManager>();
        audioSource = GetComponent<AudioSource>();
        Init();
    }

    public void Init()
    {
        // 게임 초기화
        matchedPairs = 0;
        canSelect = false;
        firstCard = null;
        secondCard = null;
    }

    public void SelectCard(Card card)
    {
        if (card == null || card == firstCard)
            return;

        if (firstCard == null)
        {
            // 첫 번째 카드 선택
            firstCard = card;
        }
        else
        {
            // 두 번째 카드 선택
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        // 일치하는지 확인
        canSelect = false;

        if (firstCard.CardID == secondCard.CardID)
        {
            Debug.Log("일치합니다");
            // 일치하는 경우, 정답 숫자 증가 후 카드 비활성화
            matchedPairs++;
            yield return new WaitForSeconds(1f);
            firstCard.ActiveFalse();
            secondCard.ActiveFalse();

            yield return new WaitForSeconds(0.1f);
            audioSource.Play();

            if (matchedPairs >= totalPairs)
            {
                // 모든 카드를 맞춘 경우
                CardGameManager.GameClear();
            }
        }
        else
        {
            // 일치하지 않는 경우, 잠시 대기 후 카드 숨기기
            yield return new WaitForSeconds(1f);
            firstCard.Hide();
            secondCard.Hide();
        }

        // 선택된 카드 초기화
        firstCard = null;
        secondCard = null;
        canSelect = true;
    }
}
