using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Target : InterfaceData
{
    public int id = 0;
    public GameObject parent;
    public Rigidbody rigidBody;

    public abstract float TakeDamage(float damageAmount);
    public void AutoDestroy(GameObject gameObject)
    {
        if(gameObject != null) Destroy(gameObject);
        else Destroy(this.gameObject);
    }

    public void Spawn(GameObject go)
    {
        Instantiate(go, transform.position, Quaternion.identity);
    }
    public void SpawnAsChild(GameObject go)
    {
        Instantiate(go, transform.position, Quaternion.identity, transform);
    }
}
