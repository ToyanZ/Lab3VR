using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnvironmentTarget : InteractableTarget
{
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
        DataUpdate(this, 0);
        return 0;
    }

}
