using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float movementX;
    [SerializeField] float movementSpeed;
    [SerializeField] Transform groundCheck;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        movementX = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(movementX * movementSpeed * 0.1f, 0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(new Vector3(0, 10), ForceMode2D.Impulse);
        }
    }

    bool IsGrounded()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(groundCheck.position, 1);
        if (colls.Length > 1)
            return true;
        else
            return false;
    }
}
