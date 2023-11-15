using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilitySelector : MonoBehaviour
{
    public InputActionReference launchAbility;
    public List<Ability> abilities;
    public List<AbilityLauncher> launchers;
    public AbilityLauncher currentLauncher;


    private void Start()
    {
        currentLauncher = launchers[0];
    }

    private void Update()
    {
        if(launchAbility.action.WasPressedThisFrame())
        {
            currentLauncher.Launch();
        }
    }



    public void SelectAbility(int index)
    {
        for (int i=0; i<launchers.Count; i++)
        {
            launchers[i].gameObject.SetActive(false);
        }
        launchers[index].gameObject.SetActive(true);

        currentLauncher = launchers[index];
    }
}
