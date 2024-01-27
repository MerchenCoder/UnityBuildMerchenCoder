using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkNode : MonoBehaviour, INode, IFollowFlow
{
    private float moveDuration = 3.0f; // 플레이어가 이동할 총 시간 (3초)
    private bool isMoving = false; // 플레이어가 현재 이동 중인지 여부
    private float startTime; // 이동 시작 시간
    private float moveSpeed = 2.3f; // 이동 속도를 조절할 변수

    private NodeNameManager nameManager;


    //for execute
    GameObject player;
    Animator walkAnim;


    private void Start()
    {
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "ActionNode";

    }


    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }

    IEnumerator INode.Execute()
    {
        if (!GameObject.FindWithTag("Player"))
        {
            Debug.Log("player를 찾을 수 없다.");
        }
        player = GameObject.FindWithTag("Player").gameObject; // 'Player' 태그를 가진 오브젝트 찾기
        walkAnim = player.GetComponent<Animator>();
        startTime = Time.time; // 현재 시간을 시작 시간으로 설정
        walkAnim.SetInteger("WalkingSpeed", 1);
        for (float elapsedTime = Time.time - startTime; elapsedTime < moveDuration; elapsedTime = Time.time - startTime)
        {
            player.transform.parent.Translate(moveSpeed * Time.deltaTime, 0, 0);
            yield return new WaitForSeconds(0.05f);
        }
        // 5초가 지나면 이동 중지
        walkAnim.SetInteger("WalkingSpeed", 0);
        yield return null;
    }
}
