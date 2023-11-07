using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMod : Ability
{
    public enum ActiveMode { Once, Permanent}
    
    [Space(20)]
    public ActiveMode activeMode = ActiveMode.Once;
    public float damagePerSecond = 15;
    public override void Activate()
    {
        //switch (activeMode)
        //{
        //    case ActiveMode.Once:

        //        break;
        //    case ActiveMode.Permanent:
        //        StartCoroutine(ActiveIE());
        //        break;
        //}
        foreach (Target target in receiver)
        {
            target.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }

    IEnumerator ActiveIE()
    {
        while (true)
        {
            foreach(Target target in receiver)
            {
                target.TakeDamage(damagePerSecond * Time.deltaTime);
            }
            yield return null;
        }
    }

    public override void Deactivate()
    {
        StopAllCoroutines();
        base.Deactivate();
    }

}
