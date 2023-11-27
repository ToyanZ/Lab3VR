using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerMaster : MonoBehaviour
{
    public List<Trigger> triggers;
    public UnityEvent OnCompleted;
    private bool completed = true;

    private void Start()
    {
        completed = true;
    }
    public void UpdateTMaster()
    {
        foreach (Trigger trigger in triggers)
        {
            if (!trigger.match) completed = false;
        }

        if (completed) OnCompleted?.Invoke();
    }
}