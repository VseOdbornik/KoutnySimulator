using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float movementX;
    float lastMovementX = 1;
    bool isWallJumping;
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

        transform.Translate(new Vector3(movementX * movementSpeed * 0.1f, 0));
        
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
            if (IsTouchingSomething(groundCheck)) Jump();
            else if (IsTouchingSomething(rightWallCheck) && isHolding) WallJump(false);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Hold();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopHolding();
        }
        FlipCharacter(lastMovementX);
    }

    void FlipCharacter(float leftOrRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * leftOrRight;
        transform.localScale = scale;
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0, 12), ForceMode2D.Impulse);
    }

    void Hold()
    {
        if (!IsTouchingSomething(rightWallCheck) || isWallJumping) return;

        isHolding = true;
        rb.velocity = Vector3.zero;
        print("Holding");
    }

    void StopHolding()
    {
        isHolding = false;
        print("NotHolding");
    }

    void WallJump(bool toRight)
    {
        StopHolding();
        StartCoroutine(WallJumping());
        if (toRight)
            rb.velocity += new Vector2(5, 7);
        else if(!toRight)
            rb.velocity += new Vector2(-5, 7);
    }

    bool IsTouchingSomething(Transform checkSide)
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(checkSide.position, checkSize);
        if (colls.Length > 1)
            return true;
        else
            return false;
    }

    IEnumerator WallJumping()
    {
        isWallJumping = true;
        yield return new WaitForSeconds(1);
        isWallJumping = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkSize);
        //Gizmos.DrawWireSphere(leftWallCheck.position, checkSize);
        Gizmos.DrawWireSphere(rightWallCheck.position, checkSize);
    }
}
