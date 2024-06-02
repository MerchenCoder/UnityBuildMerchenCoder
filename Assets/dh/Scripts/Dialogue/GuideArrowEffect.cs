using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideArrowEffect : MonoBehaviour
{
    [SerializeField] private bool activeOnce;
    public bool ActiveOnce => activeOnce;
    [SerializeField] private GameObject quizInfoPanel;
    private Image arrowImg;
    private bool isClicked = false;
    public bool IsClicked
    {
        get => isClicked;
        set => isClicked = value;
    }

    private void Start()
    {
        arrowImg = GetComponent<Image>();
        if (transform.parent.name.StartsWith("quiz") && quizInfoPanel == null)
            quizInfoPanel = transform.parent.GetComponent<QuizActive>().quizInfoPanel;
    }
    private void OnEnable()
    {
        if (quizInfoPanel != null)
            quizInfoPanel.GetComponent<CloseButton>().OnClose += OnQuizPanelClosed;
        StartCoroutine(GuideEffect());


    }
    private void OnDisable()
    {
        if (quizInfoPanel != null)
            quizInfoPanel.GetComponent<CloseButton>().OnClose -= OnQuizPanelClosed;

    }


    private IEnumerator GuideEffect()
    {
        while (true)
        {
            if (!isClicked)
            {
                yield return StartCoroutine(FadeInOut.Fade(arrowImg, 1, 0, 0.5f));
                yield return StartCoroutine(FadeInOut.Fade(arrowImg, 0, 1, 0.5f));
            }
            else break;
        }
    }

    private void OnQuizPanelClosed(bool isClosed)
    {
        isClicked = false;
        Debug.Log("구독한 이벤트에 추가한 리스터 호출");
        StartCoroutine(GuideEffect());
    }
}