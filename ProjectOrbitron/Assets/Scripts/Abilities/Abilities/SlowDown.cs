using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : Ability
{
    List<IA_Enemies> currentEnemies;

    public override void InvokeAbility()
    {
        currentEnemies = new List<IA_Enemies>();
        rigidBody.velocity = Vector3.zero;
        rigidBody.isKinematic = true;
    }

    public override void Activate()
    {
        foreach(Target target in receiver)
        {
            IA_Enemies d;
            if(target.gameObject.TryGetComponent<IA_Enemies>(out d))
            {
                if (!currentEnemies.Contains(d))
                {
                    currentEnemies.Add(d);
                    d.enemy.speed /= 4;
                }
            }
        }
    }

    public override void Deactivate()
    {
        foreach(IA_Enemies enemy in currentEnemies)
        {
            enemy.enemy.speed *= 4;
        }
        base.Deactivate();
    }

}
