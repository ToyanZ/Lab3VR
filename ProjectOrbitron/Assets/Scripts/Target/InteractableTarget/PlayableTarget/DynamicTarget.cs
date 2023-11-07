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
        new OnDataUpdated("HealthUpdated", 0, new UnityEvent<InterfaceData>())
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
        }
        else
        {
            rest = damageAmount - stats[0].current;
            stats[0].current = 0;

            switch (dieMode)
            {
                case DieMode.Destroy:
                    if (parent != null)
                    {
                        //parent.SetActive(false);
                        Destroy(parent, 1f);
                    }
                    else
                    {
                        //gameObject.SetActive(false);
                        Destroy(gameObject, 1f);
                    }
                    break;
                case DieMode.Deactivate:
                    if (parent != null)
                    {
                        parent.SetActive(false);
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;
            }
        }
        DataUpdate(this, 0);

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
