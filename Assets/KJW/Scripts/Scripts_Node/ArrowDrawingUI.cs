using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowDrawingUI : MonoBehaviour
{
    public Image arrowPrefab; // ȭ��ǥ UI ������
    private Image arrowImage; // ȭ��ǥ �̹���
    private RectTransform canvasRectTransform; // Canvas�� RectTransform

    void Start()
    {
        // Canvas�� RectTransform ��������
        canvasRectTransform = GetComponent<RectTransform>();

        // ȭ��ǥ UI �������� �����Ͽ� ���ο� ȭ��ǥ �̹��� ����
        GameObject arrowObject = Instantiate(arrowPrefab.gameObject, canvasRectTransform);
        arrowImage = arrowObject.GetComponent<Image>();
        arrowImage.gameObject.SetActive(false);
    }

    void Update()
    {
        // ���콺 ��ư�� �������� �ִ� ����
        if (Input.GetMouseButton(0))
        {
            // ���콺 ��ġ�� Canvas ���� ��ǥ�� ��ȯ
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, Camera.main, out Vector2 localPoint);

            // ȭ��ǥ �̹����� ��ġ ����
            arrowImage.rectTransform.anchoredPosition = localPoint;

            // ȭ��ǥ �̹��� Ȱ��ȭ
            arrowImage.gameObject.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // ���콺 ��ư�� �� ���, ȭ��ǥ �̹��� ��Ȱ��ȭ
            arrowImage.gameObject.SetActive(false);
        }
    }
}
