using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDestroyer : MonoBehaviour
{
    public void DestroyTargets(Trigger trigger)
    {
        List<Target> targets = trigger.GetTargets();
        for (int i=0; i<targets.Count; i++)
        {
            Destroy(targets[i].gameObject);
        }
    }
}
