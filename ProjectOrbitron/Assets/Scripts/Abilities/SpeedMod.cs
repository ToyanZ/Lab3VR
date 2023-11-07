using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMod : Ability
{
    public float slowEffect = 7;
    public override void Activate()
    {
        foreach(Target target in receiver)
        {
            DynamicTarget targetTarget = target as DynamicTarget;
            if(targetTarget != null)
            {
                targetTarget.rigidBody.drag = slowEffect;
            }
        }
    }

    public override void Deactivate()
    {
        foreach(Target target in receiver)
        {
            DynamicTarget targetTarget = target as DynamicTarget;
            if(targetTarget != null)
            {
                targetTarget.rigidBody.drag = targetTarget.physicsStats[1].current;
            }
        }
        base.Deactivate();
    }
}
