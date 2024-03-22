using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 moveVelocity = Vector3.zero;
    Animator animator;
    SpriteRenderer spriteRenderer;

    [NonSerialized] public bool isMovingLeft;
    [NonSerialized] public bool isMovingRight;

    private void Start()
    {
        isMovingLeft = false;
        isMovingRight = false;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 이전 씬에 따른 플레이어 위치 조정
        if (SceneChange.Instance.beforeScene == "1_2_town" && SceneManager.GetActiveScene().name == "1_1_farmer") { this.transform.localPosition = new Vector3(48.1f, -1.015f, 0); this.GetComponent<SpriteRenderer>().flipX = false; }
        else if (SceneChange.Instance.beforeScene == "1_3_castle" && SceneManager.GetActiveScene().name == "1_2_town") { this.transform.localPosition = new Vector3(48.6f,-1.015f, 0); this.GetComponent<SpriteRenderer>().flipX = false; }
        else if (SceneChange.Instance.beforeScene == "1_4_forest" && SceneManager.GetActiveScene().name == "1_3_castle") { this.transform.localPosition = new Vector3(26.84f, -1.015f, 0); this.GetComponent<SpriteRenderer>().flipX = false; }
    }

    public void MoveLeftDown()
    {
        animator.SetBool("isWalking", true);
        isMovingLeft = true;
    }

    public void MoveLeftUp()
    {
        animator.SetBool("isWalking", false);
        isMovingLeft = false;
    }

    public void MoveRightDown()
    {
        animator.SetBool("isWalking", true);
        isMovingRight = true;
    }

    public void MoveRightUp()
    {
        animator.SetBool("isWalking", false);
        isMovingRight = false;
    }

    private void Update()
    {
        if (isMovingLeft)
        {
            spriteRenderer.flipX = false;
            moveVelocity = new Vector3(-1f, 0, 0);
            transform.position += moveVelocity * speed * Time.deltaTime;
        }
        else if (isMovingRight)
        {
            spriteRenderer.flipX = true;
            moveVelocity = new Vector3(1f, 0, 0);
            transform.position += moveVelocity * speed * Time.deltaTime;
        }
    }
}
