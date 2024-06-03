using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AnnaWalk : MonoBehaviour
{
    bool isWalking = false;
    float targetX = 67f; // 목표 X 좌표
    float speed = 10f; // 이동 속도
    public GameObject afterDialogue;

    void Update()
    {
        if (!GameManager.Instance.CheckPlayProgress("Mission2-1-1") && !isWalking)
        {
            afterDialogue.SetActive(false);
            isWalking = true; // 움직이는 중임을 표시
            //Debug.Log("조건 충족 걷기 시작");
        }
        else if(GameManager.Instance.CheckPlayProgress("Mission2-1-1"))
        {
            isWalking = false;
            this.transform.localPosition = new Vector3(targetX, -1.72f, 0f);
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        if (isWalking)
        {
            Walking();
            //Debug.Log("걷는 중");
        }
    }

    void Walking()
    {
        // 목표 지점으로 이동
        float step = speed * Time.deltaTime; // 프레임 간 이동량 계산
        transform.localEulerAngles = new Vector3(0, 180, 0);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(targetX, -1.63f, 0), step);

        // 목표 위치에 도달하면 isWalking을 false로 설정
        if (Mathf.Abs(transform.localPosition.x - targetX) < 1f)
        {
            isWalking = false;
            this.transform.localPosition = new Vector3(targetX, -1.72f, 0f);
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
            //Debug.Log("다 걸음");
            afterDialogue.SetActive(true);
        }
    }
}