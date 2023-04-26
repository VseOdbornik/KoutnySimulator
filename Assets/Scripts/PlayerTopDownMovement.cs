using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDownMovement : MonoBehaviour
{
    Vector3 movement;
    Vector3 movementDirection;
    [SerializeField] float playerSpeed;

    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movementDirection = movement.normalized;

        transform.position += movementDirection * Time.deltaTime * playerSpeed;
    }
}
