using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelector : MonoBehaviour
{
    public List<Ability> abilities;
    public List<AbilityLauncher> launchers;
    public AbilityLauncher currentLauncher;

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
