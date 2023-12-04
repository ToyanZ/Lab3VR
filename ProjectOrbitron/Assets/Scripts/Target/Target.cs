using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Target : InterfaceData
{
    public int id = 0;
    public GameObject parent;
    public Rigidbody rigidBody;

    public abstract float TakeDamage(float damageAmount);
}
