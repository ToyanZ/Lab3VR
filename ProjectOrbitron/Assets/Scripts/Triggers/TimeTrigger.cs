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
    public bool playOnStart = true;

    [Space(20)]
    public List<OnDataUpdated> onDataUpdatedEvents = new List<OnDataUpdated>
    {
        new OnDataUpdated("Start", 0, new UnityEvent<InterfaceData>()),
        new OnDataUpdated("Counting", 0, new UnityEvent<InterfaceData>()),
        new OnDataUpdated("End", 0, new UnityEvent<InterfaceData>())
    };

    private bool counting = false;

    private void Start()
    {

        onDataUpdatedEventsPrivate = onDataUpdatedEvents;

        if (playOnStart)
        {
            Begin();
        }
    }

    public void Begin()
    {
        if (counting) return;
        DataUpdate(this, 0);
        StartCoroutine(Counter());
    }


    IEnumerator Counter()
    {
        counting = true;
        while(current < time)
        {
            current += Time.deltaTime;

            if (current < time) DataUpdate(this, 1);
            else
            {
                match = true;
                DataUpdate(this, 2);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
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
