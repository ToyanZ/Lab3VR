using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using System.Security.Cryptography;

public class Player : MonoBehaviour
{
    //public PlayerInput playerInput;
    //[Space(20)]
    //public Rigidbody2D rb;
    //public Camera cam;
    //public float speedMod = 0;
    //public InputActionReference shootInputAction;
    
    public Target target;
    public Weapon weapon;
    public CooldownLauncher abilityLauncher;

    [Space(20)]
    public InputActionReference shootInputActionSim;
    public InputActionReference shootInputAction;

    [Space(20)]
    public InputActionReference holdInputActionSim;
    public InputActionReference holdInputAction;

    public bool pressing = false;



    [Space(20)]
    public InputControls inputActions;

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


    private bool shooting = false;
    private void Update()
    {
        //Rotate();
        //Move();
        //Shoot();
        if (!GameManager.instance.useSimulator)
        {
            shooting = shootInputAction.action.IsPressed(); 
            pressing = holdInputAction.action.IsPressed();
        }
        else
        {
            shooting = shootInputActionSim.action.IsPressed();
            pressing = holdInputActionSim.action.IsPressed();
        }

    }

    private void FixedUpdate()
    {
        if (shooting) weapon.Shoot();
    }


    //private void FixedUpdate()
    //{
    //    if (direction != Vector2.zero)
    //    {
    //        rb.velocity = direction * (speed + speedMod) * Time.fixedDeltaTime;
    //    }
    //}


}

