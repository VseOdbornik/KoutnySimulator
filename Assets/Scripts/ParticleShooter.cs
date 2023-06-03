using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleShooter : PlayerComponent
{
    [SerializeField] Particle[] Particles;
    int _particleOrder;
    int ParticleOrder
    {
        get { return _particleOrder; }
        set 
        { 
            _particleOrder = value;
            if (_particleOrder >= Particles.Length) _particleOrder= 0;
        }
    }

    [SerializeField] float fireRate = 1f;
    [SerializeField] bool canShoot;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && canShoot)
        {
            ShootParticle(Particles[ParticleOrder]);
        }
    }

    IEnumerator FireWait()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    void ShootParticle(Particle particle)
    {
        particle.gameObject.SetActive(true);

        particle.transform.position = transform.position + new Vector3(Random.Range(0f, 0.1f), Random.Range(0f, 0.1f));

        particle.SetDirection(new Vector3(player.facingDirection, 0));

        particle.ChangeDamage(player.stats.Damage);

        ParticleOrder++;

        StartCoroutine(FireWait());
    }
}
