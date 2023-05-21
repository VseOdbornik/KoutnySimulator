using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject head;
    public GameObject torso;
    public GameObject legs;

    public BoxCollider2D bodyCollider;
    public BoxCollider2D armCollider;

    [HideInInspector] public Rigidbody2D rb;

    [HideInInspector] public PlayerMovement movement;
    [HideInInspector] public PlayerAnimation animations;
    [HideInInspector] public PlayerChecks checks;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();

        movement= GetComponent<PlayerMovement>();
        movement.player = this;

        animations= GetComponent<PlayerAnimation>();
        animations.player = this;

        checks = GetComponent<PlayerChecks>();
        checks.player = this;
    }
}
