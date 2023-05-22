using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : PlayerComponent
{
    PlayerAnimation playerAnimation;
    PlayerChecks checks;
    PlayerStats stats;

    Rigidbody2D rb;
    BoxCollider2D bodyCollider;
    Transform playerHeadTransform;

    [SerializeField] float walkCoefficient = 300;
    [SerializeField] float jumpCoefficient = 11;
    [SerializeField] float wallJumpCoefficient = 1.8f;

    float facingDirectionX = 1;
    [SerializeField] Vector2 dir;

    [SerializeField] float slideSpeed;

    [SerializeField] float coyoteTime = .1f;
    [SerializeField] float coyoteTimeCounter = .1f;

    [SerializeField] bool onGround;
    [SerializeField] bool onWall;
    [SerializeField] bool isHanging;
    [SerializeField] bool isHolding;
    [SerializeField] bool isCrouching;
    [SerializeField] bool cantWalk;
    [SerializeField] bool wallJumped;
    [SerializeField] bool canUncrouch;
    [SerializeField] bool canClimbHigher;
    
    private void Start()
    {
        rb = player.rb;
        playerAnimation = player.animations;
        checks = player.checks;
        bodyCollider = player.bodyCollider;
        playerHeadTransform = player.head.transform;
        stats= player.stats;
    }

    private void FixedUpdate()
    {
        SetDirection();

        if (!cantWalk)
        {
            Walk();
        }
    }

    private void Update()
    {
        onGround = checks.OnGround();
        onWall = checks.OnWall();
        isHanging = checks.IsHanging();
        canUncrouch = checks.CanUncrouch();
        canClimbHigher = checks.CanClimbHigher();

        if (!IsHolding()) StopHold();
        else StartHolding();

        if(!IsCrouching()) StopCrouching();
        else StartCrouching();

        if(isCrouching) playerAnimation.Crouch();
        else if (isHanging) playerAnimation.Hanging();
        else if (dir.x != 0 && onGround) playerAnimation.Walk();
        else if (dir.y != 0 && isHolding) playerAnimation.HoldWalk();
        else if (isHolding) playerAnimation.Hold();
        else playerAnimation.Idle();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (onGround)
        {
            coyoteTimeCounter = coyoteTime;
            StopWallJump();
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0) rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    }

    void SetDirection() 
    {
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (dir.x != 0) facingDirectionX = dir.x;

        if(!isHolding) FlipCharacter(facingDirectionX);
    }

    void Walk()
    {
        Vector2 walk = dir * Time.deltaTime * stats.WalkSpeed * walkCoefficient;

        if (!isHolding)
        { 
            if(!isCrouching) rb.velocity = new Vector2(walk.x, rb.velocity.y);
            else rb.velocity = new Vector2(walk.x / 2, rb.velocity.y);
        }
        else
        {
            if (dir.y > 0 && !canClimbHigher) return;
            rb.velocity = new Vector2(rb.velocity.x, walk.y) / 2;
        }
    }

    void Jump()
    {
        if (onGround || coyoteTimeCounter > 0)
        {
            coyoteTimeCounter = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += Vector2.up * jumpCoefficient;
        }
        else if (onWall)
        {
            bool hold = isHolding;
            StopHold();
            StartWallJump(0.3f);

            rb.velocity = Vector2.zero;
            if (hold)
            {
                rb.velocity += new Vector2(dir.x, 1).normalized * jumpCoefficient * stats.JumpForce;
            }
            else
            {
                rb.velocity += new Vector2(-facingDirectionX, 1).normalized * jumpCoefficient * stats.JumpForce;
            }
        }
    }

    void StartCrouching()
    {
        isCrouching = true;

        bodyCollider.size = new Vector2(bodyCollider.size.x, 2.05f);
        bodyCollider.offset = new Vector2(bodyCollider.offset.x, -0.21f);
        playerHeadTransform.localPosition = new Vector2(playerHeadTransform.localPosition.x, 0.32f);
    }

    void StopCrouching()
    {
        if (!canUncrouch) return;

        isCrouching = false;
        bodyCollider.size = new Vector2(bodyCollider.size.x, 2.83f);
        bodyCollider.offset = new Vector2(bodyCollider.offset.x, 0.18f);
        playerHeadTransform.localPosition = new Vector2(playerHeadTransform.localPosition.x, 0.95f);
    }

    void StartHolding()
    {
        isHolding = true;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
    }

    void StopHold()
    {
        isHolding = false;
        rb.gravityScale = 2.2f;
    }

    void FlipCharacter(float leftOrRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * leftOrRight;
        transform.localScale = scale;
    }

    bool IsHolding()
    {
        return onWall && Input.GetKey(KeyCode.LeftShift) && !wallJumped && !isCrouching;
    }

    bool IsCrouching()
    {
        return onGround && Input.GetKey(KeyCode.S) && !wallJumped && !isHolding && !isHanging;
    }

    void StartWallJump(float time)
    {
        StopCoroutine(WallJump(time));
        StartCoroutine(WallJump(time));
    }

    IEnumerator WallJump(float time)
    {
        wallJumped = true;
        yield return new WaitForSeconds(time);
        wallJumped = false;
    }

    void StopWallJump()
    {
        StopCoroutine("WallJump");
        wallJumped = false;
    }

    void StartCantWalk(float time)
    {
        StopCoroutine(CantWalk(time));
        StartCoroutine(CantWalk(time));
    }

    IEnumerator CantWalk(float time)
    {
        cantWalk = true;
        yield return new WaitForSeconds(time);
        cantWalk = false;
    }

    
}
