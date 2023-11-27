using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class AbilitySelector : AbilityController
{

    [Space(20)]
    public InputActionReference launchAbilitySim;
    public InputActionReference launchAbility;
    public List<AbilityLauncher> launchers;
    public AbilityLauncher currentLauncher;
    public float cooldownTime = 7;

    public List<OnDataUpdated> onDataUpdatedEvents = new List<OnDataUpdated>
    {
        new OnDataUpdated("Cooling", 0, new UnityEvent<InterfaceData>())
    };


    private bool cooling = false;
    private float time = 0;
    
    private void Start()
    {
        foreach (AbilityLauncher launcher in launchers)
        {
            launcher.gameObject.SetActive(false);
        }
        launchers[0].gameObject.SetActive(true);
        currentLauncher = launchers[0];

        onDataUpdatedEventsPrivate = onDataUpdatedEvents;
    }

    private void Update()
    {
        if (launchAbilitySim.action.WasPressedThisFrame())
        {
            currentLauncher.Launch();
        }

        if (launchAbility.action.WasPressedThisFrame())
        {
            currentLauncher.Launch();
        }
    }


    public void SelectAbility(int index)
    {
        if(!cooling)
        {
            for (int i = 0; i < launchers.Count; i++)
            {
                launchers[i].gameObject.SetActive(false);
                launchers[i].RestartCooldown();
            }
            launchers[index].gameObject.SetActive(true);
            currentLauncher = launchers[index];

            StartCoroutine(Cooldown());
        }
        
    }


    IEnumerator Cooldown()
    {
        cooling = true;
        time = 0;
        while (time < cooldownTime)
        {
            time += Time.deltaTime;
            DataUpdate(this, 0);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        cooling = false;
    }

    public override float GetCurrentValue()
    {
        return time;
    }
    public override float GetMaxValue()
    {
        return cooldownTime;
    }
}
