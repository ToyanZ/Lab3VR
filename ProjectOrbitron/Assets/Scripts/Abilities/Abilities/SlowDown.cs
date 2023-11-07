using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : Ability
{
    public override void Activate()
    {
        foreach(Target target in receiver)
        {
            IA_Enemies d = null;
            if(target.gameObject.TryGetComponent<IA_Enemies>(out d))
            {
                //d.enemy.speed = d.enemy.speed / 4;
                d.KillEnemy();
            }
        }
    }
}
