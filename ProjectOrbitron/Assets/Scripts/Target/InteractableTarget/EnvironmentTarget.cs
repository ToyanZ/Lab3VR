using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTarget : InteractableTarget
{
    public override float TakeDamage(float damageAmount)
    {
        DataUpdate(this, 0);
        return 0;
    }

}
