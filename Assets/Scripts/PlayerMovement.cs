using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float movementX;
    float lastMovementX = 1;
    //bool isWallJumping;
    bool isHolding;
    [SerializeField] float movementSpeed;
    [SerializeField] float checkSize;
    [SerializeField] Transform groundCheck;
    //[SerializeField] Transform leftWallCheck;
    [SerializeField] Transform rightWallCheck;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(!isHolding) movementX = Input.GetAxisRaw("Horizontal");

        if(movementX != 0) lastMovementX= movementX;

        Walk();
        
        if (movementX != 0)
        {
            Vector2 currentVelocity = rb.velocity;
            currentVelocity.x = 0f;
            rb.velocity = currentVelocity;
        }

        //rb.AddForce(transform.position + new Vector3(movementX * movementSpeed, 0), ForceMode2D.Impulse);
        //transform.position += new Vector3(movementX * movementSpeed * 0.1f, 0);
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StopHolding();
            if (IsTouchingSomething(rightWallCheck)) WallJump();
            else if (IsTouchingSomething(groundCheck)) Jump();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Hold();
        }
        FlipCharacter(lastMovementX);
    }

    void FlipCharacter(float leftOrRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * leftOrRight;
        transform.localScale = scale;
    }

    void Walk()
    {
        rb.velocity = new Vector2(movementX * movementSpeed * Time.deltaTime, rb.velocity.y);
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0, 12), ForceMode2D.Impulse);
    }

    void Hold()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopHolding();
            return;
        }
        if (!IsTouchingSomething(rightWallCheck) /*|| isWallJumping*/) return;

        isHolding = true;
        rb.velocity = Vector3.zero;
        rb.gravityScale = 0;
    }

    void StopHolding()
    {
        isHolding = false;
        rb.gravityScale = 2;
    }

    void WallJump()
    {
        //StartCoroutine(WallJumping());
        rb.velocity += new Vector2(-5 * lastMovementX, 7);
        print("WJ");
    }

    bool IsTouchingSomething(Transform checkSide)
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(checkSide.position, checkSize);
        if (colls.Length > 1)
            return true;
        else
            return false;
    }
    /*
    IEnumerator WallJumping()
    {
        isWallJumping = true;
        yield return null;
        isWallJumping = false;
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkSize);
        //Gizmos.DrawWireSphere(leftWallCheck.position, checkSize);
        Gizmos.DrawWireSphere(rightWallCheck.position, checkSize);
    }
}
