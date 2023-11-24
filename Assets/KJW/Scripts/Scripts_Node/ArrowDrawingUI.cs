using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowDrawingUI : MonoBehaviour
{
    public Image arrowPrefab; // 화살표 UI 프리팹
    private Image arrowImage; // 화살표 이미지
    private RectTransform canvasRectTransform; // Canvas의 RectTransform

    void Start()
    {
        // Canvas의 RectTransform 가져오기
        canvasRectTransform = GetComponent<RectTransform>();

        // 화살표 UI 프리팹을 복제하여 새로운 화살표 이미지 생성
        GameObject arrowObject = Instantiate(arrowPrefab.gameObject, canvasRectTransform);
        arrowImage = arrowObject.GetComponent<Image>();
        arrowImage.gameObject.SetActive(false);
    }

    void Update()
    {
        // 마우스 버튼이 눌러지고 있는 동안
        if (Input.GetMouseButton(0))
        {
            // 마우스 위치를 Canvas 상의 좌표로 변환
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, Camera.main, out Vector2 localPoint);

            // 화살표 이미지의 위치 설정
            arrowImage.rectTransform.anchoredPosition = localPoint;

            // 화살표 이미지 활성화
            arrowImage.gameObject.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // 마우스 버튼을 뗀 경우, 화살표 이미지 비활성화
            arrowImage.gameObject.SetActive(false);
        }
    }
}
