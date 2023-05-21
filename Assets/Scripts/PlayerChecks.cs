using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecks : PlayerComponent
{

    [SerializeField] Transform groundCheck;
    [SerializeField] Transform wallCheck;
    [SerializeField] Transform hangCheck;
    [SerializeField] Transform uncrouchCheck;
    [SerializeField] Transform climbCheck;

    [SerializeField] Vector2 groundCheckBounds;
    [SerializeField] Vector2 wallCheckBounds;
    [SerializeField] Vector2 hangCheckBounds;
    [SerializeField] Vector2 uncrouchCheckBounds;
    [SerializeField] Vector2 climbCheckBounds;

    public bool OnGround()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll(groundCheck.position, groundCheckBounds, 0);
        return (colls.Length > 1);
    }

    public bool OnWall()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll(wallCheck.position, wallCheckBounds, 0);
        return (colls.Length > 1);
    }

    public bool CanClimbHigher()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll(climbCheck.position, climbCheckBounds, 0);
        return (colls.Length > 1);
    }

    public bool IsHanging()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll(hangCheck.position, hangCheckBounds, 0);
        return (colls.Length > 1);
    }

    public bool CanUncrouch()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll(uncrouchCheck.position, uncrouchCheckBounds, 0);
        return (colls.Length < 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckBounds);
        Gizmos.DrawWireCube(wallCheck.position, wallCheckBounds);
        Gizmos.DrawWireCube(hangCheck.position, hangCheckBounds);
        Gizmos.DrawWireCube(uncrouchCheck.position, uncrouchCheckBounds);
        Gizmos.DrawWireCube(climbCheck.position, climbCheckBounds);
    }
}
