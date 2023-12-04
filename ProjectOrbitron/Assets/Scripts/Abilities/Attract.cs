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
    Vector3 startVelocity;

    private void Start()
    {
        startVelocity = GameManager.instance.player.weapon.GetAimDirection().normalized;
    }


    private void FixedUpdate()
    {
        rigidBody.velocity = startVelocity * speed;
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
                    Vector3 direction = transform.position - target.transform.position;
                    target.rigidBody.AddForce(direction.normalized * attractionForce, ForceMode.Impulse);
                    print(target.name);
                }
            }
        }

        //foreach(Target target in receiver)
        //{
            
        //    InteractableTarget iTarget = target as InteractableTarget;
        //    if(iTarget != null)
        //    {
               
        //    }
        //}
    }

    public override void Deactivate()
    {
        foreach(IA_Enemies enemy in currentEnemies)
        {
            enemy.enemy.speed *= 4;
        }
        base.Deactivate();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
