using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkNode : MonoBehaviour, INode, IFollowFlow
{

    private float moveDuration = 5.0f; // 플레이어가 이동할 총 시간 (5초)
    private bool isMoving = false; // 플레이어가 현재 이동 중인지 여부
    private float startTime; // 이동 시작 시간
    private float moveSpeed = 10.0f; // 이동 속도를 조절할 변수

    private NodeNameManager nameManager;


    //for execute
    GameObject player;
    Animator walkAnim;


    private void Start()
    {
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "ActionNode";

    }

    public void Execute()
    {
        MoveRight();

    }

    private void MoveRight()
    {
        if (!isMoving) // 이미 이동 중이 아닐 때만
        {
            if (!GameObject.FindWithTag("Player"))
            {
                Debug.Log("player를 찾을 수 없다.");
            }
            player = GameObject.FindWithTag("Player").gameObject; // 'Player' 태그를 가진 오브젝트 찾기
            walkAnim = player.GetComponent<Animator>();
            isMoving = true;
            startTime = Time.time; // 현재 시간을 시작 시간으로 설정
        }


    }


    private void Update()
    {
        if (isMoving)
        {
            walkAnim.SetInteger("WalkingSpeed", 1);
            float elapsedTime = Time.time - startTime;

            if (elapsedTime < moveDuration)
            {
                // 플레이어 이동
                player.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                // 5초가 지나면 이동 중지
                isMoving = false;
                walkAnim.SetInteger("WalkingSpeed", 0);
            }
        }
    }



    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }

}
