using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeableLauncher : AbilityLauncher
{
    public float maxLoad = 200;
    public float load = 0;
    public float treshhold = 40;
    public float loadRatePerSecond = 25;
    Ability clone;

    [Space(20)]
    public List<OnDataUpdated> onDataUpdatedEvents = new List<OnDataUpdated>
    {
        new OnDataUpdated("Launched", 0, new UnityEngine.Events.UnityEvent<InterfaceData>())
    };
    private void Start()
    {
        onDataUpdatedEventsPrivate = onDataUpdatedEvents;
        clone = Instantiate(ability);
    }
    public override void Launch()
    {
        if(load > treshhold)
        {
            clone.gameObject.SetActive(true);
            //clone.Activate();
        }
    }

    private void FixedUpdate()
    {
        if (!clone.gameObject.activeSelf && load < maxLoad)
        {
            load += loadRatePerSecond * Time.fixedDeltaTime;
            if (load >= maxLoad) load = maxLoad;
        }
    }
}
