using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : Ability
{
    public float speed = 40;
    Player player;
    Vector3 currentDirection;

    public override void InvokeAbility()
    {
        player = sender.GetComponent<Player>();
        currentDirection = player.weapon.GetAimDirection();
    }

    public override void Activate()
    {
        if (player.pressing)
        {
            rigidBody.velocity = player.weapon.GetAimDirection() * speed;
        }
        else
        {
            rigidBody.velocity = rigidBody.velocity.normalized * speed;
        }
    }
}
