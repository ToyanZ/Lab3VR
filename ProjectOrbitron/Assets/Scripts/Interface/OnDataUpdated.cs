using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnDataUpdated
{
    public string name;
    public int statIndex;
    public UnityEvent<InterfaceData> Event;

    //Constructor
    public OnDataUpdated(string name, int statIndex, UnityEvent<InterfaceData> unityEvent)
    {
        this.name = name;
        this.statIndex = statIndex;
        Event = unityEvent;
    }
}
