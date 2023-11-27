using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Explotion : Ability
{
    [Space(20)]
    public float radius = 2;
    public float damage = 70;
    public float speed = 80;
    public float force = 7;
    public float jumpForce = 5;
    List<Rigidbody> currentEnemies;
    public ParticleSystem particles;
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
        currentEnemies = new List<Rigidbody>();
        rigidBody.velocity = Vector3.zero;
        rigidBody.isKinematic = true;
    }

    public override void Activate()
    {
        Instantiate(particles, transform.position, Quaternion.identity);

        List<Collider> colliders =  Physics.OverlapSphere(transform.position, radius).ToList();

        foreach (Collider collider in colliders)
        {

            DynamicTarget targetDynamic;
            if(collider.gameObject.TryGetComponent<DynamicTarget>(out targetDynamic))
            {
                if (!currentEnemies.Contains(targetDynamic.rigidBody))
                {
                    currentEnemies.Add(targetDynamic.rigidBody);
                    Vector2 targetPos = new Vector2(targetDynamic.transform.position.x, targetDynamic.transform.position.z);
                    Vector2 bulletPos = new Vector2(transform.position.x, transform.position.z);

                    Vector3 rawDirection = (targetPos - bulletPos).normalized;
                    Vector3 direction = new Vector3(rawDirection.x, jumpForce, rawDirection.y);

                    IA_Enemies ia = collider.gameObject.GetComponent<IA_Enemies>();
                    if (ia != null) targetDynamic.rigidBody.AddForce((direction * force) + Vector3.up, ForceMode.Impulse);

                    targetDynamic.TakeDamage(damage);

                }
            }

        }

        foreach (Target target in receiver)
        {
            
        }

    }

    public override void Deactivate()
    {
        base.Deactivate();
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}