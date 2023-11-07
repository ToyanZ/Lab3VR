using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Target : InterfaceData
{
    int layerMask = 0;
    public GameObject parent;

    public abstract float TakeDamage(float damageAmount);
}
