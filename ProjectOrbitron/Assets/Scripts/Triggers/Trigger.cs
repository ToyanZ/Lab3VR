using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public abstract class Trigger : InterfaceData
{
    public enum Type { Enter, Load, LoadDone, Stay, StayDone, Exit}
    [Space(20)]
    public float loadTime = 0;
    public float stayTime = 0;
    //public bool useGlobalLoadTime = true;

    [Space(20)]
    protected bool enterLocked = false;
    protected bool loadingLocked = false;
    protected bool stayLocked = false;
    protected bool exitLocked = false;
    protected float load = 0;
    protected float stay = 0;

    [SerializeField] protected List<Target> targets;

    public Target GetLastTarget()
    {
        return targets[targets.Count];
    }
    public List<Target> GetTargets()
    {
        return targets;
    }
    public void EnterLock() { enterLocked = true; }
    public void LoadLock() { loadingLocked = true; }
    public void StayLock() { stayLocked = true; }
    public void ExitLock() { exitLocked = true; }

    public int ToIndex(string name)
    {
        return onDataUpdatedEventsPrivate.IndexOf(onDataUpdatedEventsPrivate.Find(x => x.name == name));
    }
    public void Print(string message)
    {
        Debug.Log("Inspector Message [" + message + "]");
    }
}
