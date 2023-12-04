using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
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

    
    private bool shooting = false;
    private void Update()
    {
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

}

