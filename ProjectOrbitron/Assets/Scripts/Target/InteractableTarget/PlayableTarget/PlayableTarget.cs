using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableTarget : InteractableTarget
{
    public enum DieMode { Destroy, Deactivate}
    public DieMode dieMode = DieMode.Deactivate;
    public float GetMaxHealth()
    {
        return stats[0].max;
    }
    public float GetHealth()
    {
        return stats[0].current;
    }
    public void SetHealth(float health)
    {
        stats[0].current = health;
    }
}
