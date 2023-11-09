using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : Ability
{
    [Space(20)]
    public float force = 150;
    List<Rigidbody> currentEnemies;

    public override void InvokeAbility()
    {
        currentEnemies = new List<Rigidbody>();
        rigidBody.velocity = Vector3.zero;
        rigidBody.isKinematic = true;
    }

    public override void Activate()
    {
        foreach (Target target in receiver)
        {
            DynamicTarget targetDynamic = target as DynamicTarget;

            if (targetDynamic == null) continue;
            if (!currentEnemies.Contains(targetDynamic.rigidBody))
            {
                currentEnemies.Add(targetDynamic.rigidBody);
                Vector2 direction = (target.transform.position - transform.position).normalized;

                IA_Enemies ia = target.gameObject.GetComponent<IA_Enemies>();
                if(ia != null)
                {
                    float speed = ia.enemy.speed;
                    ia.enemy.speed = 0;
                    targetDynamic.rigidBody.AddForce(direction * force, ForceMode.Force);
                    ia.enemy.speed = speed;
                }
                else
                {
                    //targetDynamic.rigidBody.AddForce(direction * force, ForceMode.Impulse);
                }


                targetDynamic.TakeDamage(10);
                print(target.gameObject.name);
            }
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

}