using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public Target sender;
    public Rigidbody rigidBody;
    [SerializeField] protected List<Target> receiver;

    public virtual void InvokeAbility() {  ;}
    //public abstract void Deploy();
    public abstract void Activate();
    public virtual void Deactivate() { Destroy(gameObject); }

    public void SetTargets(Trigger trigger)
    {
        receiver = new List<Target>(trigger.GetTargets());
    }
    public void TargetUpdate(Trigger trigger)
    {
        receiver = new List<Target>(trigger.GetTargets());
    }
}
