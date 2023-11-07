using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTarget : PlayableTarget
{
    public override float TakeDamage(float damageAmount)
    {
        float rest = 0;
        if (stats[0].current - damageAmount > 0)
        {
            stats[0].current -= damageAmount;
        }
        else
        {
            rest = damageAmount - stats[0].current;
            stats[0].current = 0;

            switch (dieMode)
            {
                case DieMode.Destroy:
                    if (parent != null)
                    {
                        parent.SetActive(false);
                        Destroy(parent, 1f);
                    }
                    else
                    {
                        gameObject.SetActive(false);
                        Destroy(gameObject, 1f);
                    }
                    break;
                case DieMode.Deactivate:
                    if (parent != null)
                    {
                        parent.SetActive(false);
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;
            }
        }
        DataUpdate(this, 0);

        

        return rest;
    }
}
