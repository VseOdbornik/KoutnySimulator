using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Particle : MonoBehaviour
{
    public Vector3 direction;
    float particleSpeed = 10;
    public float ParticleSpeed
    {
        get { return particleSpeed; }
        set { particleSpeed = value; }
    }

    private void Start()
    {
        StopCoroutine(ParticleExpiration());
        StartCoroutine(ParticleExpiration());
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * Time.deltaTime * ParticleSpeed);
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
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
