using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageRotation : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public static Vector2 DefaultPos;
    private float pressTime = 0f; // 버튼을 누르고 있는 시간을 저장하는 변수

    public RectTransform boundaryObject; // 이동 가능한 범위를 제한할 오브젝트
    private Vector2 boundarySize; // 이동 가능한 범위 크기

    public Transform targetObject; // 목표 오브젝트
    public GameObject successPanel;
    public GameObject successImg;
    private bool isLocked = false; // 고정 여부

    public bool isOverlapping = false; // 겹치는지 여부 확인
    public bool allPiecesOverlapping = false; // 모든 퍼즐 조각이 겹치는지 여부 확인

    private Coroutine successRoutine; // 성공 루틴
    public AudioSource overlappingSFX;
    public AudioClip audioClip;
    public AudioClip successAudioClip;
    public AudioClip rotationClip;

    void Start()
    {
        // boundaryObject의 크기를 사용하여 이동 범위 설정
        if (boundaryObject != null)
        {
            SetBoundarySize();
        }
        //Debug.Log("z is " + transform.rotation.eulerAngles.z);
    }

    void SetBoundarySize()
    {
        // boundaryObject의 크기를 가져와서 이동 가능한 범위로 설정
        boundarySize = boundaryObject.rect.size / 2f;
    }


    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (!isLocked)
        {
            DefaultPos = this.transform.position;
        }
        else
        {
            transform.position = targetObject.position;
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (!isLocked)
        {
            // 현재 마우스 포인터 위치를 가져옵니다.
            Vector2 currentPos = eventData.position;

            // 이동 가능한 범위 내에서만 이동하도록 제한합니다.
            float clampedX = Mathf.Clamp(currentPos.x, boundaryObject.position.x - boundarySize.x, boundaryObject.position.x + boundarySize.x);
            float clampedY = Mathf.Clamp(currentPos.y, boundaryObject.position.y - boundarySize.y, boundaryObject.position.y + boundarySize.y);

            // 이동 가능한 범위 내에서 마우스 포인터 위치로 이미지의 위치를 설정합니다.
            transform.position = new Vector2(clampedX, clampedY);
        }
        else
        {
            transform.position = targetObject.position;
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        float distance = Vector2.Distance(transform.position, targetObject.position);
        Vector2 currentPos = eventData.position;

        //Debug.Log("Moving Piece Rotation is " + transform.rotation.eulerAngles.z);

        if (!isLocked)
        {
            // 거리가 5 픽셀 이하이고, 회전 각도가 0도인 경우에만 고정합니다.
            if (distance <= 20f && (Mathf.Approximately(transform.rotation.eulerAngles.z, 1.001791E-05f)) && !isOverlapping)
            {
                // 고정될 오브젝트의 위치로 이동합니다.
                transform.position = targetObject.position;
                // Rigidbody를 비활성화하여 더 이상 이동하지 않도록 합니다.
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                    isOverlapping = true;
                    CheckAllPiecesOverlapping();
                }
                isLocked = true;
                overlappingSFX.PlayOneShot(audioClip, 1.0f);
            }
            else
            {
                //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                this.transform.position = currentPos;
            }
        }
        else
        {
            transform.position = targetObject.position;
        }
        //Debug.Log("isLocked is " + isLocked);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressTime = Time.time; // 버튼을 누른 시간 기록
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Time.time - pressTime < 0.3f) // 1초 미만으로 눌렀을 때만 회전
        {
            //Debug.Log("PressTime ie " + pressTime);
            RotateImage();
        }
    }

    public void RotateImage()
    {
        if (!isLocked)
        {
            // 이미지의 현재 Z축 회전 각도
            float currentRotation = transform.rotation.eulerAngles.z;
            //Debug.Log("currentRotation is " + currentRotation);

            // 90도씩 회전
            int newRotation = (int)currentRotation + 90;

            // 회전 각도를 0에서 360도 사이로 유지
            newRotation = (int)Mathf.Clamp(newRotation, 0f, 360f);
            //Debug.Log("newRotation is " + newRotation);

            // 회전 적용
            for (int i = (int)currentRotation; i <= (int)newRotation; i++)
            {
                transform.rotation = Quaternion.Euler(0, 0, i);
            }
            overlappingSFX.PlayOneShot(rotationClip, 1.0f);
        }
    }

    private void CheckAllPiecesOverlapping()
    {
        // 모든 퍼즐 오브젝트가 목표 오브젝트와 겹치는지 확인합니다.
        ImageRotation[] puzzlePieces = FindObjectsOfType<ImageRotation>();
        //Debug.Log("puzzlePieces are " + puzzlePieces);
        allPiecesOverlapping = true;

        foreach (ImageRotation piece in puzzlePieces)
        {
            if (!piece.isOverlapping)
            {
                allPiecesOverlapping = false;
                break;
            }
        }
        if (allPiecesOverlapping)
        {
            // 모든 퍼즐 오브젝트가 목표 오브젝트와 완벽하게 겹쳐졌을 때 특정 이미지로 변경할 수 있습니다.
            // 예: puzzleImage.sprite = successSprite;
            successImg.SetActive(true);

            // 성공 루틴 시작
            successRoutine = StartCoroutine(ShowSuccessPanel());
            Debug.Log("All Pieces are Overlapped");
        }
    }

    private IEnumerator ShowSuccessPanel()
    {
        // 5초 대기
        yield return new WaitForSeconds(3f);

        // 성공 패널 활성화
        overlappingSFX.PlayOneShot(successAudioClip, 1.0f);
        successPanel.SetActive(true);
    }
}