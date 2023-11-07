using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class Weapon : MonoBehaviour
{
    public Projectile projectile;
    public Transform tip;
    public Transform center;

    [Space(20)]
    [SerializeField] private float shootForce = 40;
    [SerializeField] private float fireRate = 0.02f;

    private float fireRateCount=0;
    private bool shooting = false;
    
    public void Shoot()
    {
        if(!shooting) StartCoroutine(ShootIE());
    }

    IEnumerator ShootIE()
    {
        shooting = true;


        Projectile bullet = Instantiate(projectile, tip.position, Quaternion.identity);

        Vector3 direction = (tip.position - center.position);
        Vector3 force = direction.normalized * shootForce;

        bullet.rb.AddForce(force, ForceMode.Impulse);

        yield return new WaitForSeconds(fireRate);
        
        
        shooting = false;
    }
}
