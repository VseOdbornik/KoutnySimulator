using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float movementX;
    float movementY;
    Vector2 dir;

    const int walkCoeficient = 300;
    const int jumpCoeficient = 11;

    [SerializeField] float coyoteTime = .1f;
    [SerializeField] float coyoteTimeCounter = .1f;

    float lastMovementX = 1;
    const float checkSize = 0.05f;
    [SerializeField] float movementSpeed = 1;
    [SerializeField] float jumpForce = 1;
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform rightWallCheck;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
        dir = new Vector2(movementX, movementY);
        if (movementX != 0) lastMovementX= movementX;

        Walk();
        FlipCharacter(lastMovementX);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        if (IsGrounded()) coyoteTimeCounter = coyoteTime;
        else coyoteTimeCounter -= Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0) rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    }

    void Walk()
    {
        rb.velocity = new Vector2(movementX * movementSpeed * Time.deltaTime * walkCoeficient, rb.velocity.y);
    }

    void Jump()
    {
        if (!IsGrounded() && coyoteTimeCounter <= 0) return;

        coyoteTimeCounter = 0;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce * jumpCoeficient;
    }

    void FlipCharacter(float leftOrRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * leftOrRight;
        transform.localScale = scale;
    }

    bool IsGrounded()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll(groundCheck.position, new Vector2(0.65f, 0.2f),0);
        if (colls.Length > 1)
            return true;
        else
            return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, new Vector2(0.65f, 0.2f));
        //Gizmos.DrawWireSphere(rightWallCheck.position, checkSize);
    }
}
