using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityController : InterfaceData
{
    public Target sender;
    public List<Ability> abilities;
    [SerializeField] protected int currentAbilityIndex = 0;

    public void SetCurrentAbility(int index)
    {
        currentAbilityIndex = index;
    }
    public void NextAbility()
    {
        currentAbilityIndex += currentAbilityIndex < abilities.Count - 1 ? 1 : -currentAbilityIndex;
    }
    public void PrevAbility()
    {
        currentAbilityIndex += currentAbilityIndex > 1 ? -1 : abilities.Count - 1;

    }
}
