using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : Ability
{
    public float dieTime = 3;
    public float speed = 40;
    Player player;
    Vector3 currentDirection;

    bool triggered = false;
    public override void InvokeAbility()
    {
        
    }

    private void Start()
    {
        player = sender.GetComponent<Player>();
        currentDirection = player.weapon.GetAimDirection();
        StartCoroutine(Travel());
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(dieTime);
        Destroy(gameObject);
    }


    IEnumerator Travel()
    {
        while (!triggered)
        {
            if (player.pressing)
            {
                rigidBody.velocity = player.weapon.GetAimDirection().normalized * speed;
                print("A");
            }
            else
            {
                rigidBody.velocity = rigidBody.velocity.normalized * speed;
                print("B");
            }
            yield return null;
        }
        
    }

    public override void Activate()
    {
        triggered = true;

        foreach (Target target in receiver)
        {
            DynamicTarget targetDynamic = target as DynamicTarget;

            if (targetDynamic == null) continue;
            targetDynamic.TakeDamage(10);

        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}
