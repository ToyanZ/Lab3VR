using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class OverlapTrigger : Trigger
{
    [Space(20)]
    public List<OnDataUpdated> onDataUpdatedEvents = new List<OnDataUpdated> 
    {
        new OnDataUpdated(Type.Enter.ToString(), 0, new UnityEvent<InterfaceData>()),
        new OnDataUpdated(Type.Load.ToString(), 0, new UnityEvent<InterfaceData>()),
        new OnDataUpdated(Type.LoadDone.ToString(), 0, new UnityEvent<InterfaceData>()),
        new OnDataUpdated(Type.Stay.ToString(), 1, new UnityEvent<InterfaceData>()),
        new OnDataUpdated(Type.StayDone.ToString(), 1, new UnityEvent<InterfaceData>()),
        new OnDataUpdated(Type.Exit.ToString(), 0, new UnityEvent<InterfaceData>()),
        new OnDataUpdated(Type.Signal.ToString(), 0, new UnityEvent<InterfaceData>())
    };

    private void Start()
    {
        targets = new List<Target>();
        onDataUpdatedEventsPrivate = onDataUpdatedEvents;
    }

    private void FixedUpdate()
    {
        if (targets.Count > 0)
        {
            if (load < loadTime)
            {
                if (loadingLocked) return;

                load += Time.fixedDeltaTime;
                DataUpdate(this, ToIndex(Type.Load.ToString()));
                if (load >= loadTime) DataUpdate(this, ToIndex(Type.LoadDone.ToString()));
            }
            else
            {
                if (stayLocked) return;

                if (stayTime == 0)
                {
                    DataUpdate(this, ToIndex(Type.Stay.ToString()));
                }
                else if (stay < stayTime)
                {
                    stay += Time.fixedDeltaTime;
                    DataUpdate(this, ToIndex(Type.Stay.ToString()));

                    if (stay > stayTime) DataUpdate(this, ToIndex(Type.StayDone.ToString())); ;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (enterLocked) return;

        Target target = collision.GetComponent<Target>();
        if (target != null)
        {
            contactPoint = Vector3.zero;
            if (target != ignoreThis)
            {
                if (!targets.Contains(target)) targets.Add(target);
                match = target.id == id;
                contactPoint = collision.ClosestPointOnBounds(transform.position);
                DataUpdate(this, ToIndex(Type.Enter.ToString()));
                load = 0;
            }
        }

        switch (mode)
        {
            case Mode.Pulse:
                signal = true;
                DataUpdate(this, ToIndex(Type.Signal.ToString()));
                signal = false;
                break;
            case Mode.Switch:
                signal = !signal;
                if (signal) DataUpdate(this, ToIndex(Type.Signal.ToString()));
                break;
        }

    }
    private void OnTriggerExit(Collider collision)
    {
        if (exitLocked) return;

        Target target = collision.GetComponent<Target>();
        if (target != null)
        {
            targets.Remove(target);
            DataUpdate(this, ToIndex(Type.Exit.ToString()));
            load = 0;
        }

        if (mode == Mode.Pulse) signal = false;
    }

   

    public override float GetMaxValue()
    {
        switch(lastIndexCalled)
        {
            case 0: return load;
            case 1: return stay;
            default: return 1;
        }
    }
    public override float GetCurrentValue()
    {
        switch (lastIndexCalled)
        {
            case 0: return loadTime;
            case 1: return stayTime;
            default: return 1;
        }
    }
    public override bool GetBoolValue() { return signal; }
    public override string GetDisplayValue() { return name; }

    public void LogUpdate()
    {
        print("happening");
    }
}
