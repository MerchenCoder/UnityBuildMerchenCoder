using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(isMovingLeft)
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
