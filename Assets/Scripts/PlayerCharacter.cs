using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using InControl;

public class PlayerCharacter : MonoBehaviour
{
    //incontrol
    TwoAxisInputControl filteredDirection;

    public enum State
    {
        None,
        Idle,
        Walk,
        Jump,
        Die
    }

    private State currentState_ = State.None;

    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private PlayerFoot foot;
    [SerializeField] private Transform respawnPoint;
    public Vector3 RespawnPos => respawnPoint.position;

    private const float DeadZone = 0.1f;
    private const float MoveSpeed = 5.0f;
    private const float JumpSpeed = 7.0f;

    private bool facingRight_ = true;
    private bool jumpButtonDown_ = false;

    //incontrol
    void Awake()
    {
        filteredDirection = new TwoAxisInputControl();
        filteredDirection.StateThreshold = 0.5f;
    }

    void Start()
    {
        ChangeState(State.Jump);
    }

    private void Update()
    {
        // Use last device which provided input.
        var inputDevice = InputManager.ActiveDevice;
        filteredDirection.Filter(inputDevice.Direction, Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpButtonDown_ = true;
        }
    }

    void FixedUpdate()
    {
        float moveDir = 0.0f;
        if /*(Input.GetKey(KeyCode.LeftArrow) ||*/ (filteredDirection.Left.WasPressed)
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
                else if (foot.SpikeContact > 0)
                {
                    ChangeState(State.Die);
                }
                break;
            case State.Die:
                if (foot.SpikeContact == 0)
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
            case State.Die:
                anim.Play("Die");
                StartCoroutine(DeathDelay());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

        currentState_ = state;
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(0.2f);
        transform.position = respawnPoint.transform.position;
    }

    void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        facingRight_ = !facingRight_;
    }
}
