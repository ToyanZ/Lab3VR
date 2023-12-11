using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootDisc : MonoBehaviour
{
    public Transform tip;
    public GameObject disc;
    public float speed;
    public InputActionReference shootInputAction;
    public InputActionReference shootInputActionSim;
    private bool shooting = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        shooting = shootInputAction.action.IsPressed();
 
    }
    void FixedUpdate()
    {
        if (shooting)
        {
            shootDisc();
        }
    }

    public void shootDisc()
    {
        GameObject clone = Instantiate(disc, Vector3.zero, Quaternion.identity);
        Rigidbody rb = clone.GetComponent<Rigidbody>();
        rb.AddForce(tip.forward * speed, ForceMode.Impulse);
    }
}
