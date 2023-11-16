using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Events;

public class TimeTrigger : Trigger
{

    public float time = 0;
    float current = 0;

    [Space(20)]
    public List<OnDataUpdated> onDataUpdatedEvents = new List<OnDataUpdated>
    {
        new OnDataUpdated("Start", 0, new UnityEvent<InterfaceData>()),
        new OnDataUpdated("Counting", 0, new UnityEvent<InterfaceData>()),
        new OnDataUpdated("End", 0, new UnityEvent<InterfaceData>())
    };

    private void Start()
    {
        onDataUpdatedEventsPrivate = onDataUpdatedEvents;
        DataUpdate(this, 0);
    }

    private void Update()
    {
        if (current >= time) return;

        current += Time.deltaTime;

        if (current < time) DataUpdate(this, 1);
        else DataUpdate(this, 2);
    }



    public override float GetMaxValue()
    {
        return time;
    }
    public override float GetCurrentValue()
    {
        return current;
    }
    public override bool GetBoolValue() { return signal; }
    public override string GetDisplayValue() { return name; }

    public void LogUpdate()
    {
        print("happening");
    }
}
