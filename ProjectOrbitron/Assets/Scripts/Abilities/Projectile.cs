using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Ability
{
    public float damage = 10;



    public override void Activate()
    {
        print("2");
        if (receiver.Count == 0) return;
        try
        {
            foreach (Target target in receiver)
            {
                if (target == null) continue;
                target.TakeDamage(damage);

                IA_Enemies d = null;
                if (target.gameObject.TryGetComponent<IA_Enemies>(out d))
                {
                    //d.enemy.speed = d.enemy.speed / 4;
                    d.KillEnemy();
                    print("2");
                }
                print("1");
            }
        } catch (Exception e) { }
        gameObject.SetActive(false);
        Destroy(gameObject, 0.1f);
    }
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject, 0.1f);
    }
}
