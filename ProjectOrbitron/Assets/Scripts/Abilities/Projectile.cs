using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Ability
{
    public float damage = 10;


    public override void Activate()
    {
        if (receiver.Count == 0) return;
        try
        {
            foreach (Target target in receiver)
            {
                if (target == null) continue;
                float reward = target.TakeDamage(damage);
                
                if (reward > 0)
                {
                    GameManager.instance.player.abilityLauncher.CooldownFaster();
                }
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
