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

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isAlive) 
        {
            enemy.SetDestination(Player.position);
            Debug.Log("Esta vivo");
        }
    }
    public void KillEnemy()
    {
        if (isAlive)
        {
            Debug.Log("Mata al enemigo");
            isAlive = false;
            anim.SetTrigger("Death");
            enemy.speed = 0;
        }
    }
}
