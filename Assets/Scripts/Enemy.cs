using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected float collisionDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            collision.GetComponent<Player>().stats.TakeDamage(collisionDamage);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }
}
