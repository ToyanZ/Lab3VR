using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_Enemies : MonoBehaviour
{
    public NavMeshAgent enemy;

    public Transform Player;

    public bool isAlive;

    public Animator anim;
    public float currentSpeed;

    public int id_Attack;

    [Space(20)]
    public float damage = 0.02f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        currentSpeed = enemy.speed;


        //Player = GameManager.instance.player.transform;

    }

    private void Update()
    {
        if(isAlive && Player != null) 
        {
            //enemy.SetDestination(Player.position);
            enemy.destination = Player.position;
        }
    }
    public void KillEnemy()
    {
        WavesManager.instance.remainingEnemies--;
        isAlive = false;
        anim.SetTrigger("Death");
        enemy.speed = 0;
    }

    public void Attack()
    {
        id_Attack = Random.Range(0, 2);
        anim.SetInteger("id_Attack", id_Attack);

        if (isAlive)
        {
            anim.SetTrigger("Attack");
        }
    }

    IEnumerator ReturnWalking()
    {
        float returnToWalk = id_Attack == 0 ? 1.5f : 1f;
        enemy.speed = 0;
        yield return new WaitForSeconds(returnToWalk);
        enemy.speed = currentSpeed;
    }



    private void OnCollisionEnter(Collision collision)
    {
        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.target.TakeDamage(damage);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.target.TakeDamage(damage);
        }
    }


}
