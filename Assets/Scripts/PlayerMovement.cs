using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] Transform groundCheck;
    [SerializeField] Transform wallCheck;

    [SerializeField] float walkCoefficient = 300;
    [SerializeField] float jumpCoefficient = 11;
    [SerializeField] float wallJumpCoefficient = 1.8f;

    float facingDirectionX = 1;
    Vector2 dir;

    [SerializeField] float movementSpeed = 1;
    [SerializeField] float jumpForce = 1;
    [SerializeField] float slideSpeed;
    [SerializeField] float wallJumpTime = .5f;
    [SerializeField] int wallJumpMoveSpeed;

    [SerializeField] float coyoteTime = .1f;
    [SerializeField] float coyoteTimeCounter = .1f;

    [SerializeField] bool onGround;
    [SerializeField] bool onWall;
    [SerializeField] bool isHolding;
    [SerializeField] bool cantWalk;
    [SerializeField] bool wallJumped;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        SetBools();

        if (Input.GetKeyUp(KeyCode.LeftShift) || !onWall)
        {
            StopHold();
        }

        if(Input.GetKey(KeyCode.LeftShift) && onWall && !wallJumped)
        {

            if (!isHolding) StartHolding();
            //else Hold();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround || coyoteTimeCounter > 0)
            {
                Jump();
            }
            else if(onWall)
            {
                WallJump();
            } 
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

        if(!isHolding)
        FlipCharacter(facingDirectionX);
    }

    void SetBools()
    {
        onGround = OnGround();
        onWall = OnWall();
        isHolding = IsHolding();
    }

    void Walk()
    {
        Vector2 walk = dir * movementSpeed * Time.deltaTime * walkCoefficient;

        if (!isHolding)
        {
            rb.velocity = new Vector2(walk.x, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, walk.y) / 2;
        }
    }

    void Jump()
    {
        coyoteTimeCounter = 0;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce * jumpCoefficient;
    }

    void WallJump()
    {
        bool hold = isHolding;
        StopHold();
        StartWallJump(0.3f);

        rb.velocity = Vector2.zero;
        if (hold)
        {
            rb.velocity += new Vector2(dir.x, 1).normalized * jumpForce * jumpCoefficient;
        }
        else
        {
            rb.velocity += new Vector2(-facingDirectionX, 1).normalized * jumpForce * jumpCoefficient;
        }
    }

    void StartHolding()
    {
        isHolding = true;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
    }

    void Hold()
    {
        //rb.velocity = new Vector2(0, rb.velocity.y);
    }

    void StopHold()
    {
        rb.gravityScale = 2.2f;
        isHolding = false;
    }

    void FlipCharacter(float leftOrRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * leftOrRight;
        transform.localScale = scale;
    }

    bool OnGround()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll(groundCheck.position, new Vector2(0.65f, 0.12f), 0);
        if (colls.Length > 1) return true;
        else return false;
    }

    bool OnWall()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll(wallCheck.position, new Vector2(0.1f, 1f), 0);
        if (colls.Length > 1) return true;
        else return false;
    }

    bool IsHolding()
    {
        return onWall && Input.GetKeyDown(KeyCode.LeftShift);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, new Vector2(0.65f, 0.12f));
        Gizmos.DrawWireCube(wallCheck.position, new Vector2(0.1f, 1f));
    }
}
