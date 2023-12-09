using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Attract : Ability
{
    List<IA_Enemies> currentEnemies;

    public float speed = 80;
    public float radius = 3;
    public float attractionForce = 30;
    public ForceMode forceMode = ForceMode.Impulse;
    Vector3 startVelocity;

    private void Start()
    {
        startVelocity = GameManager.instance.player.weapon.GetAimDirection().normalized;
    }


    private void FixedUpdate()
    {
        if (!rigidBody.isKinematic) rigidBody.velocity = startVelocity * speed;
    }



    public override void InvokeAbility()
    {
        currentEnemies = new List<IA_Enemies>();
        rigidBody.velocity = Vector3.zero;
        rigidBody.isKinematic = true;
    }

    public override void Activate()
    {
        Collider[] colliders =  Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            Target target = collider.GetComponent<Target>();
            if(target != null)
            {
                if (target.rigidBody != null)
                {
                    IA_Enemies enemy = target.GetComponent<IA_Enemies>();
                    if (enemy != null) { enemy.enemy.enabled = false; }

                    Vector3 direction = (transform.position - target.transform.position);
                    float distance = direction.magnitude;
                    float forceMagnitude = (rigidBody.mass * target.rigidBody.mass) / (distance * distance);
                    Vector3 force = direction.normalized * forceMagnitude;

                    Vector3 impactPosition = target.rigidBody.ClosestPointOnBounds(transform.position);
                    
                    
                    target.rigidBody.AddForceAtPosition(force, impactPosition, forceMode);

                    //if (enemy != null) { enemy.enemy.enabled = true; }
                }
            }
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
