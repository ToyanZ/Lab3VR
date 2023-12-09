using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : Ability
{
    public float dieTime = 3;
    public float speed = 40;
    public float impactForce = 30;

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
            if (player.holdGrip)
            {
                rigidBody.velocity = player.weapon.GetAimDirection().normalized * speed;
            }
            else
            {
                rigidBody.velocity = rigidBody.velocity.normalized * speed;
            }
            yield return null;
        }
        
    }

    public override void Activate()
    {
        triggered = true;

        foreach (Target target in receiver)
        {
            if (target == null) continue;
            target.TakeDamage(10);
            if(target.rigidBody != null)
            {
                target.rigidBody.AddForce(rigidBody.velocity.normalized * impactForce, ForceMode.Impulse);
                break;
            }
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

}
