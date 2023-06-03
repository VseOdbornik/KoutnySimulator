using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{

    public Vector3 direction;
    float speedMultiplier = 10;

    float damage;

    Vector3 addedSpeed;
    Vector3 addedSpeedRaw;

    private void OnEnable()
    {
        ResetParticle();
    }

    public void ResetParticle()
    {
        StopCoroutine(ParticleExpiration());
        damage = 0;
        direction= Vector3.zero;
        addedSpeed= Vector3.zero;
        addedSpeedRaw= Vector3.zero;
        StartCoroutine(ParticleExpiration());
    }

    private void FixedUpdate()
    {
        transform.position += ((direction + addedSpeed) * speedMultiplier + addedSpeedRaw) * Time.deltaTime;
    }

    public void ChangeDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void ChangeSpeed(Vector3 speedChange)
    {
        addedSpeed += speedChange;
    }

    public void ChangeSpeedRaw(Vector3 speedChange)
    {
        addedSpeedRaw += speedChange;
    }

    IEnumerator ParticleExpiration()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) collision.GetComponent<Enemy>().TakeDamage(damage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) gameObject.SetActive(false);
        print("Particle collided");
    }
}
