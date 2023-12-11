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
        foreach ( GameObject disc in GameObject.FindGameObjectsWithTag("Disc"))
        {
            Destroy(disc);
        }
        GameObject clone = Instantiate(disc, tip.position , disc.transform.rotation);
        Rigidbody rb = clone.GetComponent<Rigidbody>();
        rb.AddForce(tip.forward * speed, ForceMode.Impulse);
    }
}
