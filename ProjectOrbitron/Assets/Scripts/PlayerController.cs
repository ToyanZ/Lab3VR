using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputControls inputControls;
    public PlayerInput playerInput;
    public DynamicTarget target;
    public float walkSpeed = 20f;

    [Space(20)]
    public CustomInputAction walk;
    public CustomInputAction shoot;
    public CustomInputAction reload;
    public CustomInputAction grip;
    //public CustomInputAction ability;

    //public CustomInputAction abilityOne;
    //public CustomInputAction abilityTwo;
    //public CustomInputAction abilityThree;

    [Space(20)]
    public CustomInputAction select;

    private void Start()
    {

    }


    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    playerInput.SwitchCurrentActionMap("GUIInteraction");
        //}

        walk.pressed = walk.input.action.IsPressed();
    }

    private void FixedUpdate()
    {
        Walk();
    }
    
    public void Walk()
    {
        if (walk.pressed)
        {
            Vector2 rawDirection = walk.input.action.ReadValue<Vector2>().normalized;
            Vector3 direction = new Vector3(rawDirection.x, 0, rawDirection.y);
            target.rigidBody.velocity = direction * walkSpeed;
        }
        else target.rigidBody.velocity = Vector3.zero;
    }

}

[System.Serializable]
public class CustomInputAction
{
    public InputActionReference input;
    public bool pressed;
}