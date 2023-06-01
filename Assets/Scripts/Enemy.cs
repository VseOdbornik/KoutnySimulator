using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health;
    public float Damage;
    public float CollisionDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<Player>().stats.TakeDamage(CollisionDamage);
        }
    }

    protected virtual void TakeDamage(float damage)
    {
        Health += damage;
    }
}
