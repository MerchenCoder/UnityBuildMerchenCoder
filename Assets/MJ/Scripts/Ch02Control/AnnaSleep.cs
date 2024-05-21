using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnaSleep : MonoBehaviour
{
    // 자식 객체들을 관리하기 위한 리스트
    private List<Transform> childObjects = new List<Transform>();

    // 이미지가 들어 있는 객체에 대한 참조
    public GameObject imageObject;

    private RectTransform imageRectTransform;

    void Start()
    {
        // 자식 객체들을 리스트에 추가
        foreach (Transform child in transform)
        {
            if (child.gameObject != imageObject)
            {
                childObjects.Add(child);
            }
        }
        if (imageObject != null)
        {
            imageRectTransform = imageObject.GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        if (GameManager.Instance.CheckPlayProgress("Chap2Start") || !DataManager.Instance.gameStateData.ch2MissionClear[0])
        {
            RotateImage();
            gameObject.transform.localPosition = new Vector3(12.4144f, -3.564708f, 0);
            FlipImageY(true);
        }
        else
        {
            ResetImageRotation();
            if(GameManager.Instance.CheckPlayProgress("Mission2-1Y"))
                FlipImageY(true);
            else
                FlipImageY(false);
        }
    }

    void RotateImage()
    {
        // AnnaSleepObject를 회전
        if (imageObject != null)
        {
            transform.localEulerAngles = new Vector3(0, 0, 270);

            // 하위 객체들을 역회전시켜 원래 상태로 유지
            foreach (Transform child in childObjects)
            {
                child.localEulerAngles = new Vector3(0, 0, -270);
            }
        }
    }

    void ResetImageRotation()
    {
        // AnnaSleepObject의 회전을 초기화
        if (imageObject != null)
        {
            transform.localEulerAngles = Vector3.zero;

            // 하위 객체들의 회전도 초기화
            foreach (Transform child in childObjects)
            {
                child.localEulerAngles = Vector3.zero;
            }
        }
    }

    void FlipImageY(bool flip)
    {
        if (imageRectTransform != null)
        {
            Vector3 scale = imageRectTransform.localScale;
            scale.y = flip ? -1 : 1;
            imageRectTransform.localScale = scale;
        }
    }
}
