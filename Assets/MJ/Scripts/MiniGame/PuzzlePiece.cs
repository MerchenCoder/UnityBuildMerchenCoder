using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public Transform targetObject; // 목표 오브젝트
    public GameObject successPanel;

    private bool isOverlapping = false; // 겹치는지 여부 확인
    private bool allPiecesOverlapping = false; // 모든 퍼즐 조각이 겹치는지 여부 확인

    private void Update()
    {
        if (isOverlapping)
        {
            // 특정 효과음 재생 등의 동작을 수행할 수 있습니다.
            // 예: AudioManager.PlaySound("OverlapSound");

            // 퍼즐 오브젝트가 목표 오브젝트와 완벽하게 겹쳐졌을 때 이동 불가능하도록 만듭니다.
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Debug.Log("A Piece is Overlapped");
        }

        if (allPiecesOverlapping)
        {
            // 모든 퍼즐 오브젝트가 목표 오브젝트와 완벽하게 겹쳐졌을 때 특정 이미지로 변경할 수 있습니다.
            // 예: puzzleImage.sprite = successSprite;

            // 성공 패널을 활성화합니다.
            successPanel.SetActive(true);
            Debug.Log("All Pieces are Overlapped");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == targetObject)
        {
            isOverlapping = true;
            // 모든 퍼즐 오브젝트가 목표 오브젝트와 겹치는지 확인합니다.
            CheckAllPiecesOverlapping();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == targetObject)
        {
            isOverlapping = false;
        }
    }

    private void CheckAllPiecesOverlapping()
    {
        // 모든 퍼즐 오브젝트가 목표 오브젝트와 겹치는지 확인합니다.
        PuzzlePiece[] puzzlePieces = FindObjectsOfType<PuzzlePiece>();
        Debug.Log("puzzlePieces are " + puzzlePieces);
        allPiecesOverlapping = true;

        foreach (PuzzlePiece piece in puzzlePieces)
        {
            if (!piece.isOverlapping)
            {
                allPiecesOverlapping = false;
                break;
            }
        }
    }
}
