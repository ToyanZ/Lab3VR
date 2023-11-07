using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StackLauncher : AbilityLauncher
{
    public float stackMax = 4;
    public float stack = 0;

    public float loadTime = 3;
    public float load = 0;

    public List<OnDataUpdated> onDataUpdatedEvents = new List<OnDataUpdated>
    {
        new OnDataUpdated("Launched", 0, new UnityEngine.Events.UnityEvent<InterfaceData>())
    };

    private void Start()
    {
        onDataUpdatedEventsPrivate = onDataUpdatedEvents;
    }

    public override void Launch()
    {
        if (stack > 0)
        {
            Ability clone = Instantiate(ability, (Vector2)sender.transform.position + Vector2.one, Quaternion.identity);
            stack -= 1;
        }
    }
    private void FixedUpdate()
    {
        if (stack < stackMax)
        {
            load += Time.fixedDeltaTime;
            DataUpdate(this, 0);
            if(load >= loadTime)
            {
                load = 0;
                stack += 1;
            }
        }
    }

    public override float GetCurrentValue()
    {
        return load;
    }
    public override float GetMaxValue()
    {
        return loadTime;
    }
    public override string GetDisplayValue()
    {
        return inputKeyName;
    }
}
