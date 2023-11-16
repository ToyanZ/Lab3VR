using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PhysicButton : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public bool pressed = false;
    public float time = 0.1f;
    private Vector3 velocity;


   

    private void FixedUpdate()
    {
        if (pressed)
        {
            transform.position = Vector3.SmoothDamp(transform.position, end.position, ref velocity, time);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, start.position, ref velocity, time);
        }
    }


    public void Spawn(GameObject go)
    {
        Instantiate(go, transform.position, Quaternion.identity);
    }
}
