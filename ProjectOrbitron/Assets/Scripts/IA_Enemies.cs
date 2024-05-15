using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class IA_Enemies : MonoBehaviour
{
    public enum State { Walking, Idle}
    public NavMeshAgent navMeshAgent;
    public Rigidbody rBody;
    public Transform target;
    public Animator anim;
    public WaveController waveController;

    [Space(20)]
    public int id_Attack;
    public float startSpeed;
    public bool isAlive;

    [Space(20)]
    public float damageMod = 0f;

    private float baseDamage = 0;
    private State state = State.Walking; 

    private void Start()
    {
        anim = GetComponent<Animator>();
        startSpeed = navMeshAgent.speed;
        baseDamage = GameManager.instance.enemyAttackDamage;
        isAlive = true;
    }

    private void Update()
    {
        if (isAlive)
        {
            if (HasTarget() && IsWalking()) navMeshAgent.destination = target.position;
        }
        else KillEnemy();
    }
    bool HasTarget() { return target != null; }
    bool IsWalking() { return state == State.Walking; }


    public void Push(Vector3 force) { if (state == State.Walking) StartCoroutine(PushIE(force)); }
    IEnumerator PushIE(Vector3 force)
    {
        state = State.Idle;
        navMeshAgent.speed = 0;
        navMeshAgent.enabled = false;
        rBody.AddForce(force, ForceMode.Impulse);
        yield return new WaitForSeconds(GameManager.instance.enemyPauseTime);
        navMeshAgent.speed = startSpeed;
        navMeshAgent.enabled = true;
        state = State.Walking;
    }
    public void Pause() { if (state == State.Walking) StartCoroutine(PauseIE()); }
    IEnumerator PauseIE()
    {
        state = State.Idle;
        navMeshAgent.speed = 0;
        navMeshAgent.enabled = false;
        yield return new WaitForSeconds(GameManager.instance.enemyPauseTime);
        navMeshAgent.speed = startSpeed;
        navMeshAgent.enabled = true;
        state = State.Walking;
    }

    private void Attack(Player player)
    {
        if (player != null) player.target.TakeDamage(baseDamage + damageMod);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Attack(collision.gameObject.GetComponent<Player>());
    }
    private void OnCollisionStay(Collision collision)
    {
        Attack(collision.gameObject.GetComponent<Player>());
    }













    public void KillEnemy()
    {
        if(WavesManager.instance != null) WavesManager.instance.remainingEnemies--;
        if (waveController != null) waveController.remainingEnemies--;
        isAlive = false;
        anim.SetTrigger("Death");
        navMeshAgent.speed = 0;
        if (waveController != null) waveController.enemiesList.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
    public void Attack()
    {
        id_Attack = Random.Range(0, 2);
        anim.SetInteger("id_Attack", id_Attack);

        if (isAlive) anim.SetTrigger("Attack");
    }
    IEnumerator ReturnWalking()
    {
        float returnToWalk = id_Attack == 0 ? 1.5f : 1f;
        navMeshAgent.speed = 0;
        yield return new WaitForSeconds(returnToWalk);
        navMeshAgent.speed = startSpeed;
    }

    
    

}
