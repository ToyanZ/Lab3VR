using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownLauncher : AbilityLauncher
{
    public float cooldownTime = 4;
    public float cooldown = 0;
    public float cooldownModValue = 0.08f;
    private float cooldownMod = 0;

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
        cooldown = cooldownTime;
        
        Ability clone = Instantiate(abilities[0], GameManager.instance.player.weapon.tip.position, Quaternion.identity);
        clone.sender = sender;
        clone.GetComponent<Trigger>().ignoreThis = sender;

        while(cooldown > 0)
        {
            float time = Time.deltaTime + cooldownMod;
            cooldown -= time;
            cooldownMod = 0;
            DataUpdate(this, 0);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        cooldown = 0;
    }


    public void CooldownFaster()
    {
        cooldownMod = cooldownModValue;
    }

    public override void RestartCooldown()
    {
        StopAllCoroutines();
        cooldown = 0;
        DataUpdate(this, 0);
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
