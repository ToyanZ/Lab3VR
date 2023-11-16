using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaticTarget : PlayableTarget
{

    [Space(20)]
    public List<OnDataUpdated> onDataUpdatedEvents = new List<OnDataUpdated>
    {
        new OnDataUpdated("HealthUpdated", 0, new UnityEvent<InterfaceData>()),
        new OnDataUpdated("TargetDead", 0, new UnityEvent<InterfaceData>())
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
            DataUpdate(this, 0);
        }
        else
        {
            rest = damageAmount - stats[0].current;
            stats[0].current = 0;
            DataUpdate(this, 1);

            switch (dieMode)
            {
                case DieMode.Destroy:
                    if (parent != null)
                    {
                        parent.SetActive(false);
                        Destroy(parent, 1f);
                    }
                    else
                    {
                        gameObject.SetActive(false);
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
        

        

        return rest;
    }



    public void TakeDamage2(float damageAmount)
    {
        if (stats[0].current - damageAmount > 0)
        {
            stats[0].current -= damageAmount;
            DataUpdate(this, 0);
        }
        else
        {
            stats[0].current = 0;
            DataUpdate(this, 1);

            switch (dieMode)
            {
                case DieMode.Destroy:
                    if (parent != null)
                    {
                        parent.SetActive(false);
                        Destroy(parent, 1f);
                    }
                    else
                    {
                        gameObject.SetActive(false);
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
    }

    public override float GetMaxValue()
    {
        return GetMaxHealth();
    }
    public override float GetCurrentValue()
    {
        return GetHealth();
    }


}
