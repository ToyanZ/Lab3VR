using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public Target sender;
    public Rigidbody rigidBody;
    [SerializeField] protected List<Target> receiver;

    [Space(20)]
    float invokeTime = 0;
    
    float deployTime = 0;
    
    //Active = Operative + Latent
    float activeTime = 0;
    float operativeTime = 0;
    float latentTime = 0;

    //public abstract void InvokeAbility();
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
