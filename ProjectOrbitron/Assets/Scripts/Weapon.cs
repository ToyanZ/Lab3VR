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
    public InputActionReference shootInputAction;
    float fireRateCount=0;

    [Space(20)]
    public float shootForce = 40;
    public float fireRate = 0.02f;

    private void Update()
    {
        if (shootInputAction.action.IsPressed()) Shoot();

    }

    void Shoot()
    {
        fireRateCount += Time.deltaTime;
        if (fireRateCount < fireRate) return;
        fireRateCount = 0;

        Projectile bullet = Instantiate(projectile, tip.position, Quaternion.identity);
        
        Vector3 direction = (tip.position - center.position);
        Vector3 force = direction.normalized * shootForce;

        bullet.rb.AddForce(force, ForceMode.Impulse);
    }
}
