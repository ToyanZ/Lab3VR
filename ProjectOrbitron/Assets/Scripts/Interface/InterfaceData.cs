using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public abstract class InterfaceData : MonoBehaviour
{
    protected int lastIndexCalled = 0;
    //[HideInInspector] 
    public  List<OnDataUpdated> onDataUpdatedEventsPrivate;
    //public OnDataUpdatedEvent onDataUpdatedEvent;
    public virtual float GetCurrentValue() { Debug.LogError("[GetCurrentValue() Not Implemented]"); return 0; }
    public virtual float GetMaxValue() { Debug.LogError("[GetMaxValue() Not Implemented]"); return 0; }
    public virtual string GetStringValue() { Debug.LogError("[GetMaxValue() Not Implemented]"); return ""; }
    public virtual bool GetBoolValue() { Debug.LogError("[GetBoolValue() Not Implemented]"); return false; }
    public virtual string GetDisplayValue() { Debug.LogError("[GetDisplayValue() Not Implemented]"); return ""; }
    public void DataUpdate(InterfaceData interfaceData, int eventIndex)
    {
        if(eventIndex < onDataUpdatedEventsPrivate.Count && eventIndex > -1)
        {
            lastIndexCalled = onDataUpdatedEventsPrivate[eventIndex].statIndex;
            onDataUpdatedEventsPrivate[eventIndex].Event?.Invoke(interfaceData);
        }
        else
        {
            Debug.Log("Index [" + eventIndex + "] out of range on " + name + " events");
        }
        
    }

    public void Print(string s)
    {
        Debug.Log(s);
    }
}
