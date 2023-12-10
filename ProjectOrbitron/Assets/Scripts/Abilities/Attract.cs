using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Attract : Ability
{
    public float speed = 80;
    public float radius = 3;
    public float attractionForce = 30;
    public float damagePerSecond = 0.33f;
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
        rigidBody.velocity = Vector3.zero;
        rigidBody.isKinematic = true;
    }

    public override void Activate()
    {
        Collider[] colliders =  Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            //Target check
            Target target = collider.GetComponent<Target>();
            if(target == null) continue;

            //Damage target (if is other target)
            if(target != sender) target.TakeDamage(damagePerSecond * Time.fixedDeltaTime);

            //Rigidbody and Enemy check
            if (target.rigidBody == null) continue;
            IA_Enemies enemy = target.GetComponent<IA_Enemies>();
            if (enemy != null) { enemy.Pause(); }

            //Calculate and apply force
            Vector3 direction = (transform.position - target.transform.position);
            float distance = direction.magnitude;
            float forceMagnitude = (rigidBody.mass * target.rigidBody.mass) / (distance * distance);
            Vector3 force = direction.normalized * forceMagnitude;

            Vector3 impactPosition = target.rigidBody.ClosestPointOnBounds(transform.position);
            target.rigidBody.AddForceAtPosition(force, impactPosition, forceMode);
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
