using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using System.Security.Cryptography;

public class Player : MonoBehaviour
{
    public Target target;
    public Weapon weapon;
    //public PlayerInput playerInput;
    
    [Space(20)]
    //public Rigidbody2D rb;
    //public Camera cam;
    //public float speedMod = 0;

    [Space(20)]
    public InputActionReference moveInputAction;
    public InputActionReference rotationInputAction;
    //public InputActionReference shootInputAction;

    [Space(20)]
    public InputActionReference shootInputActionSim;
    public InputActionReference shootInputAction;

    [Space(20)]
    public InputActionReference pauseInputActionSim;
    public InputActionReference pauseInputAction;


    Vector2 direction;
    float speed;
    //[HideInInspector] public PlayerInputActions inputActions;
    //private void Start()
    //{
    //    cam = Camera.main;
    //    speed = GameManager.instance.playerValues.movementSpeed + speedMod;

    //    inputActions = new PlayerInputActions();
        
    //    rotationInputAction.action.performed += ctx => Rotate(ctx);
    //    moveInputAction.action.performed += ctx =>  Move(ctx);
    //    shootInputAction.action.performed += ctx => Shoot(ctx);
    //}
    //void Rotate()
    //{
    //    if (playerInput.currentControlScheme == inputActions.GamepadScheme.name.ToString())
    //    {
    //        transform.up = rotationInputAction.action.ReadValue<Vector2>();
    //    }
    //    else
    //    {
    //        Vector2 mousePos = cam.ScreenToWorldPoint(rotationInputAction.action.ReadValue<Vector2>());
    //        transform.up = (mousePos - (Vector2)transform.position).normalized;
    //    }
    //}
    //void Move()
    //{
    //    direction = moveInputAction.action.ReadValue<Vector2>().normalized;
    //}
    
    //void Shoot()
    //{
    //    if (shootInputAction.action.IsPressed()) weapon.Shoot(transform.up); 
    //}


    
    private void Update()
    {
        //Rotate();
        //Move();
        //Shoot();
        if (!GameManager.instance.useSimulator)
        {
            //if (pauseInputAction.action.WasPressedThisFrame()) InterfaceManager.instance.PauseMenu();
            if (shootInputAction.action.WasPressedThisFrame()) weapon.Shoot();
        }
        else
        {
            //if (pauseInputActionSim.action.WasPressedThisFrame()) InterfaceManager.instance.PauseMenu();
            if (shootInputActionSim.action.WasPressedThisFrame()) weapon.Shoot();
        }
    }

    //private void FixedUpdate()
    //{
    //    if (direction != Vector2.zero)
    //    {
    //        rb.velocity = direction * (speed + speedMod) * Time.fixedDeltaTime;
    //    }
    //}



    
    

}
