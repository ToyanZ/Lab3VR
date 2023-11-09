using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownLauncher : AbilityLauncher
{
    public float cooldownTime = 4;
    public float cooldown = 0;

    [Space(20)]
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
        if (cooldown == 0) StartCoroutine(LaunchIE());
    }

    IEnumerator LaunchIE()
    {
        Ability clone = Instantiate(abilities[0], (Vector2)sender.transform.position + Vector2.one, Quaternion.identity);
        cooldown = cooldownTime;
        while(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            DataUpdate(this, 0);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        cooldown = 0;
    }

    public override float GetCurrentValue()
    {
        return cooldown;
    }
    public override float GetMaxValue()
    {
        return cooldownTime;
    }
    public override string GetDisplayValue()
    {
        return inputKeyName;
    }
}
