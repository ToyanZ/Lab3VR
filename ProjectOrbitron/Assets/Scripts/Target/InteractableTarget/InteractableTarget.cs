using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableTarget : Target
{
    public Stat[] stats = new Stat[1] 
    {
        new Stat(Stat.Type.Number, "health", 0, 200, false, "health")
    };

    

}
