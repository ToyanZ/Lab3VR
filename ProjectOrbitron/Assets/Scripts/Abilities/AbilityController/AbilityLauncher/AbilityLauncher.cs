using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityLauncher : AbilityController
{
    public string inputKeyName = "-";
    public abstract void Launch();
}
