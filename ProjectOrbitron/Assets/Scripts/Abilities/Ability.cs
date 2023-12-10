using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public Target sender;
    public Rigidbody rigidBody;
    [SerializeField] protected List<Target> receiver;
    [HideInInspector] public Vector3 contactPoint;

    public virtual void InvokeAbility() {  ;}
    //public abstract void Deploy();
    public abstract void Activate();
    public virtual void Deactivate() { Destroy(gameObject); }

    public void SetTargets(Trigger trigger)
    {
        receiver = new List<Target>(trigger.GetTargets());
    }
    public void SetTriggerPos(Trigger trigger)
    {
        contactPoint = trigger.contactPoint;
    }
    public void TargetUpdate(Trigger trigger)
    {
        receiver = new List<Target>(trigger.GetTargets());
    }
}
