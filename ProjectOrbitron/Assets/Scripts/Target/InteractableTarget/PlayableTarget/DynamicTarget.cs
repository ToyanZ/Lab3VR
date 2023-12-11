using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DynamicTarget : PlayableTarget
{
    public RigidbodyType2D rigidbodyType = RigidbodyType2D.Dynamic;
    public Stat[] physicsStats = new Stat[4] 
    {
        new Stat(Stat.Type.Number, "mass", 0, 1, false, "--"),
        new Stat(Stat.Type.Number, "dragg", 0, 0.05f, false, "--"),
        new Stat(Stat.Type.Number, "angularDrag", 0, 0.05f, false, "--"),
        new Stat(Stat.Type.Number, "grvityScale", 0, 1, false, "--"),
    };

    [Space(20)]
    public List<OnDataUpdated> onDataUpdatedEvents = new List<OnDataUpdated>
    {
        new OnDataUpdated("HealthUpdated", 0, new UnityEvent<InterfaceData>()),
        new OnDataUpdated("TargetDead", 1, new UnityEvent<InterfaceData>())
    };

    private void Start()
    {
        stats[0].current = stats[0].max;
        onDataUpdatedEventsPrivate = onDataUpdatedEvents;
    }
    public override float TakeDamage(float damageAmount)
    {
        float rest = 0;
        


        if (stats[0].current - damageAmount > 0)
        {
            stats[0].current -= damageAmount;
            rest = damageAmount/2.0f;
            DataUpdate(this, 0);
        }
        else
        {
            rest = (damageAmount - stats[0].current)/2;
            stats[0].current = 0;
            DataUpdate(this, 0);
            DataUpdate(this, 1);

            switch (dieMode)
            {
                case DieMode.Destroy:
                    if (parent != null) Destroy(parent, 1f);
                    else Destroy(gameObject, 1f);
                    //print(name + " dead.");
                    break;
                case DieMode.Deactivate:
                    if (parent != null) parent.SetActive(false);
                    else gameObject.SetActive(false);
                    break;
            }
        }
        

        return rest;
    }

    public override float GetCurrentValue()
    {
        return stats[0].current;
    }
    public override float GetMaxValue()
    {
        return stats[0].max;
    }
    public override string GetDisplayValue()
    {
        return stats[0].displayName;
    }
}
