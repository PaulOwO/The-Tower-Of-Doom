using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCharacter : MonoBehaviour
{
    public enum State
    {
        None,
        Idle,
        Walk,
        Jump
    }

    private State currentState_ = State.None;

    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private PlayerFoot foot;

    private const float DeadZone = 0.1f;
    private const float MoveSpeed = 2.0f;
    private const float JumpSpeed = 5.0f;

    private bool facingRight_ = true;
    private bool jumpButtonDown_ = false;

    void Start()
    {
        ChangeState(State.Jump);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpButtonDown_ = true;
        }
    }

    void FixedUpdate()
    {
        float moveDir = 0.0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir -= 1.0f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDir += 1.0f;
        }

        if (foot.FootContact > 0 && jumpButtonDown_)
        {
            Jump();
        }
        jumpButtonDown_ = false;

        var vel = body.velocity;
        body.velocity = new Vector2(MoveSpeed * moveDir, vel.y);
        //We flip the characters when not facing in the right direction
        if (moveDir > DeadZone && !facingRight_)
        {
            Flip();
        }

        if (moveDir < -DeadZone && facingRight_)
        {
            Flip();
        }
        //We manage the state machine of the character
        switch (currentState_)
        {
            case State.Idle:
                if (Mathf.Abs(moveDir) > DeadZone)
                {
                    ChangeState(State.Walk);
                }

                if (foot.FootContact == 0)
                {
                    ChangeState(State.Jump);
                }
                break;
            case State.Walk:
                if (Mathf.Abs(moveDir) < DeadZone)
                {
                    ChangeState(State.Idle);
                }

                if (foot.FootContact == 0)
                {
                    ChangeState(State.Jump);
                }
                break;
            case State.Jump:
                if (foot.FootContact > 0)
                {
                    ChangeState(State.Idle);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Jump()
    {
        var vel = body.velocity;
        body.velocity = new Vector2(vel.x, JumpSpeed);
    }

    void ChangeState(State state)
    {
        switch (state)
        {
            case State.Idle:
                anim.Play("Idle");
                break;
            case State.Walk:
                anim.Play("Walk");
                break;
            case State.Jump:
                anim.Play("Jump");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

        currentState_ = state;
    }

    void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        facingRight_ = !facingRight_;
    }
}
