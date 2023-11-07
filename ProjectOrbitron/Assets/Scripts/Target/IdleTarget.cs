using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTarget : Target
{
    public override float TakeDamage(float damageAmount) { return 0; }
}
