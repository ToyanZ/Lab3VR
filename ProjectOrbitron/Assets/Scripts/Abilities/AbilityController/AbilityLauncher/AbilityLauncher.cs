using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public abstract class AbilityLauncher : AbilityController
{
    public string inputKeyName = "-";
    public abstract void Launch();
    public virtual void RestartCooldown() { Debug.Log("Not implemented"); }
}
