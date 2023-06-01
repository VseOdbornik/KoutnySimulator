using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public Vector3 direction;
    float speedMultiplier = 10;

    Vector3 addedSpeed;
    Vector3 addedSpeedRaw;

    private void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        StopCoroutine(ParticleExpiration());
        direction= Vector3.zero;
        addedSpeed= Vector3.zero;
        addedSpeedRaw= Vector3.zero;
        StartCoroutine(ParticleExpiration());
    }

    private void FixedUpdate()
    {
        transform.Translate((direction + addedSpeed) * Time.deltaTime * speedMultiplier + addedSpeedRaw);
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
        if (collision.gameObject.layer == Layers.wallLayer) gameObject.SetActive(false);
    }
}
