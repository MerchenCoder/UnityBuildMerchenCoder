using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class FadeInOut : MonoBehaviour
{
    public Image Panel;
    float time = 0f;
    float F_time = 1f;

    private void Start()
    {
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutFlow());
    }

    IEnumerator FadeOutFlow()
    {
        Panel.gameObject.SetActive(true);
        Color alpha = new Color(0, 0, 0, 0);
        time = 0f;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        yield return null;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInFlow());
    }

    IEnumerator FadeInFlow()
    {
        Panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = new Color(0, 0, 0, 1);
        while (alpha.a > 0)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        yield return null;
        Panel.raycastTarget = false;
    }

    public static IEnumerator Fade(Image target, float start, float end, float fadeTime = 1, UnityAction action = null)
    {
        //페이드 효과를 재생할 대상(target)이 없으면 코루틴 메소드 종료
        if (target == null) yield break;

        float percent = 0;

        while (percent < 1)
        {
            //페이드 효과 재생

            percent += Time.deltaTime / fadeTime;
            Color color = target.color; //현재 색상 정보 저장
            color.a = Mathf.Lerp(start, end, percent);//색상의 alpha 값 변경
            target.color = color;//변경된 색상 반영

            yield return null;
        }

        //페이드 효과 재생이 완료되면 action 메소드가 등록되어 있는지를 확인한다.
        //등록되어 있다면 해당 메소드를 실행한다.
        action?.Invoke();

    }

}
