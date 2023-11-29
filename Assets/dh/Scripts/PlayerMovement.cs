using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigid;
    float time; //생각하는 시간을 결정하는 변수
    public int nextMove; //다음 행동지표를 결정할 변수
    public int playerSpeed = 5; //이동 스피드

    float spriteWidth;
    // bool CollisionFlag = false;

    SpriteRenderer spriteRenderer;
    Animator walkAnim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        walkAnim = GetComponent<Animator>();
        spriteWidth = spriteRenderer.bounds.size.x; //width 가져오기
        Invoke("Think", 0); // 초기화 함수 안에 넣어서 실행될 때 마다(최초 1회) nextMove변수가 초기화 되도록
    }

    private void Update()
    {

        //화면 밖으로 나가지 않도록 위치 제한
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position); //현재 위치를 뷰포트 공간 좌표로 변환
        if (viewportPos.x < 0.08f || viewportPos.x > 0.9f)
        {
            viewportPos.x = Mathf.Clamp(viewportPos.x, 0.1f, 0.9f);
            // 조정된 좌표를 월드 공간 좌표로 변환하여 다시 오브젝트의 현재 위치에 넣어준다.
            Turn();
        }



        //카메라 밖을 나가지 않도록 조정.
        // if (viewportPos.x - spriteHalfWidth < 0f)
        // {
        //     //화면 끝에 도달하면 현재 속도 0으로 변경 & 위치 조정
        //     viewportPos.x = spriteHalfWidth;
        //     rigid.velocity = new Vector2(0, rigid.velocity.y);
        // }
        // else if (viewportPos.x + spriteHalfWidth > 1f)
        // {
        //     viewportPos.x = 1f - spriteHalfWidth;
        //     rigid.velocity = new Vector2(0, rigid.velocity.y);
        // }

        transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove * playerSpeed, rigid.velocity.y); //nextMove 에 0:멈춤 -1:왼쪽 1:오른쪽 으로 이동 
    }

    // private void Update()
    // {
    //     화면 밖으로 나가는 것을 방지하는 코드
    //     float spriteHalfWidth = spriteRenderer.bounds.size.x / 2f;
    //     float positonX = transform.position.x;
    //     float positionY = transform.position.y;

    //     Vector3 viewportPosMin = Camera.main.WorldToViewportPoint(new Vector3(positonX - spriteHalfWidth, positionY, 0));
    //     Vector3 viewportPosMax = Camera.main.WorldToViewportPoint(new Vector3(positonX + spriteHalfWidth, positionY, 0));

    //     if (viewportPosMin.x < 0f || viewportPosMax.x > 1f)
    //     {

    //         Debug.Log(viewportPosMin.x);
    //         if (!CollisionFlag)
    //         {
    //             CollisionFlag = true;
    //             Turn();

    //         }
    //         transform.position = new Vector3(transform.position.x + spriteHalfWidth, positionY, 0);
    //     }
    //     else if ()
    //     {
    //         Debug.Log("충돌");
    //         // transform.position = new Vector3(transform.position.x - spriteHalfWidth, positionY, 0);
    //         Turn();
    //     }
    //     else
    //     {

    //     }

    //     // Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
    //     if (viewportPos.x - spriteHalfWidth < 0f)
    //     {
    //         Debug.Log("충돌");
    //         Turn();
    //         // viewportPos.x = 0f;
    //         // rigid.velocity = new Vector2(0, rigid.velocity.y);
    //     }
    //     else if (viewportPos.x + spriteHalfWidth > 1f)
    //     {
    //         Turn();
    //         // viewportPos.x = 1f;
    //         // rigid.velocity = new Vector2(0, rigid.velocity.y);
    //     }

    //     //조정된 좌표를 월드 공간 좌표로 변환하여 다시 오브젝트의 현재 위치에 넣어줍니다.
    //     transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
    // }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     // Debug.Log("충돌발생");
    //     // if (collision.gameObject.CompareTag("MainCamera"))
    //     // {
    //     //     Debug.Log("카메라와충돌발생");
    //     //     Turn();
    //     // }

    //     Debug.Log("Collision Detected: " + collision.gameObject.name);
    //     if (collision.gameObject.CompareTag("MainCamera"))
    //     {
    //         Debug.Log("Camera Collider Collision Detected");
    //         Turn();
    //     }
    // }



    void Think()
    { //스스로 생각해서 판단 (-1:왼쪽이동 ,1:오른쪽 이동 ,0:멈춤)
      //Random.Range : 최소보다 크거나 같고, 최대보다 작거나 같은 난수를 생성한다. 이때 최대는 제외된다.
        nextMove = Random.Range(-1, 2); //int 형이므로 소수 모두 정수형으로 변환됨

        //walking animation
        walkAnim.SetInteger("WalkingSpeed", nextMove);

        //sprite 방향
        if (nextMove != 0)
        { //0이 아닐 때만 방향을 바꿔준다. 서있을 때는 방향 바꿀 필요 없음.
            spriteRenderer.flipX = nextMove == 1;

        }

        time = Random.Range(2f, 5f); //생각하는 시간을 랜덤으로 부여

        //Think(); 재귀함수를 그냥 사용시, CPU 과부화된다. 따라서 재귀함수를 쓸 때는 항상 직접호출하는 대산 Invoke()를 사용하는 것이 좋다.
        Invoke("Think", time);
    }

    //화면 벽과 충돌했을 때 호출되는 함수
    void Turn()
    {
        Debug.Log("Turn 호출");
        //1. 이동방향 전환
        if (nextMove != 0)
        {
            nextMove = nextMove * (-1);
        }

        spriteRenderer.flipX = nextMove == 1;

        //2. 현재 작동 중인 모든 Invoke 함수 중지
        CancelInvoke();
        //3. 다시 호출
        Invoke("Think", 2);
        // CollisionFlag = false;

    }


}
