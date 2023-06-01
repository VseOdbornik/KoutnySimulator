using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleShooter : PlayerComponent
{
    [SerializeField] Particle[] Electrons;
    [SerializeField] Particle[] Holes;

    int _electronOrder;
    int ElectronOrder
    {
        get { return _electronOrder; }
        set
        {
            _electronOrder = value;
            if (_electronOrder >= Electrons.Length) _electronOrder = 0;
        }
    }
    int _holeOrder;
    int HoleOrder
    {
        get { return _holeOrder; }
        set
        {
            _holeOrder = value;
            if (_holeOrder >= Holes.Length) _holeOrder = 0;  
        }
    }

    [SerializeField] float fireRate = 1f;
    [SerializeField] bool shootElectron;
    [SerializeField] bool canShoot;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.E) && canShoot)
        {
            if(shootElectron) ShootParticle(Electrons[ElectronOrder], true);
            else ShootParticle(Holes[HoleOrder], false);
        }
    }

    IEnumerator FireWait()
    {
        canShoot= false;
        yield return new WaitForSeconds(fireRate);
        canShoot= true;
    }

    void ShootParticle(Particle particle, bool isElectron)
    {
        particle.gameObject.SetActive(true);
        particle.transform.position = transform.position + new Vector3(Random.Range(0f, 0.1f), Random.Range(0f, 0.1f));

        if (player.dir.y != 0) particle.SetDirection(new Vector3(player.dir.x, player.dir.y));
        else particle.SetDirection(new Vector3(player.facingDirection, 0));

        particle.ChangeSpeedRaw(player.rb.velocity*Time.deltaTime*5);

        shootElectron = !shootElectron;

        if (isElectron) ElectronOrder++;
        else HoleOrder++;

        StartCoroutine(FireWait());
    }
}
