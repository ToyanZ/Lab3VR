using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public abstract class Trigger : InterfaceData
{
    public Target ignoreThis;
    public bool match = false;
    public int id = 0;
    public enum Type { Enter, Load, LoadDone, Stay, StayDone, Exit, Signal}
    public enum Mode { Pulse, Switch}
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

    public Mode mode = Mode.Pulse;
    public bool signal = false;
    [HideInInspector] public Vector3 contactPoint = Vector3.zero;

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
        OnDataUpdated newEvent = onDataUpdatedEventsPrivate.Find(x => x.name == name);
        int idex = onDataUpdatedEventsPrivate.IndexOf(newEvent);
        
        return idex;
    }
    public void PrintSome(string message)
    {
        if(message == "")
        {
            if(targets.Count >0)
            {
                if (targets[0] != null) Debug.Log("Inspector Message [" + targets[0].name + "]");
            }
            
        }
        else
        {
            Debug.Log("Inspector Message [" + message + "]");
        }
    }
    public void AutoDestroy(GameObject go)
    {
        Destroy(go);
    }
    public void Spawn(GameObject go)
    {
        Instantiate(go, transform.position, Quaternion.identity);
    }
    public void SpawnAsChild(GameObject go)
    {
        Instantiate(go, transform.position, Quaternion.identity, transform);
    }


    public void StopHere()
    {
        Debug.Break();
    }
}
