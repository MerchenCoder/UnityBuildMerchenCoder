using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


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

}
