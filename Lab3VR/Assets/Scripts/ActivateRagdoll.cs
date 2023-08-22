using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRagdoll : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.isKinematic = true;
            GetComponent<Animator>().enabled = true;
        }
    }
}
