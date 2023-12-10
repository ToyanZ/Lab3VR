using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalk : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public Rigidbody rb;
    public enum State { Walking, Pause}
    private State state = State.Walking;
    private float speed = 0;


    private void Start()
    {
        speed = agent.speed;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Walking:
                if (target != null) agent.destination = target.position;
                break;
        }
        
    }

    public void Push(Vector3 force)
    {
        StartCoroutine(PushIE(force));
    }
    IEnumerator PushIE(Vector3 force)
    {
        state = State.Pause;
        agent.speed = 0;
        agent.enabled = false;
        rb.AddForce(force, ForceMode.Impulse);
        yield return new WaitForSeconds(1.5f);
        agent.speed = speed;
        agent.enabled = true;
        state = State.Walking;
    }
}
