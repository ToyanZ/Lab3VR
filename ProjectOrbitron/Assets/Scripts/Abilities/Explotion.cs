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
    List<IA_Enemies> currentEnemies;
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
        currentEnemies = new List<IA_Enemies>();
        rigidBody.velocity = Vector3.zero;
        rigidBody.isKinematic = true;
    }

    public override void Activate()
    {
        Instantiate(particles, transform.position, Quaternion.identity);

        List<Collider> colliders =  Physics.OverlapSphere(transform.position, radius).ToList();

        foreach (Collider collider in colliders)
        {
            Target target = collider.GetComponent<Target>();
            if(target != null)
            {
                if (target == sender) continue;
                else if (target.rigidBody != null)
                {
                    


                    IA_Enemies enemy = target.GetComponent<IA_Enemies>();
                    if(enemy != null) 
                    { 
                        currentEnemies.Add(enemy);
                        enemy.currentSpeed = 0; 
                    }

                    Vector2 targetPos = new Vector2(target.transform.position.x, target.transform.position.z);
                    Vector2 bulletPos = new Vector2(transform.position.x, transform.position.z);

                    Vector2 direction = (targetPos - bulletPos).normalized * force;
                    Vector3 explotionForce = new Vector3(direction.x, jumpForce, direction.y);

                    EnemyWalk enemyWalk = target.GetComponent<EnemyWalk>();
                    if(enemyWalk != null) enemyWalk.Push(explotionForce);

                    target.rigidBody.AddForce(explotionForce, ForceMode.Impulse);
                    target.TakeDamage(damage);

                    //if (enemy != null) { enemy.enemy.enabled = true; }
                }
            }
        }
    }

    public override void Deactivate()
    {
        foreach(IA_Enemies enemy in currentEnemies)
        {
            if (enemy != null) enemy.enemy.enabled = true;
        }
        base.Deactivate();
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}