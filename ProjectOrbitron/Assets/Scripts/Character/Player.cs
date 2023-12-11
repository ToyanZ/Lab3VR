using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Target target;
    public Weapon weapon;
    public CooldownLauncher abilityLauncher;

    [Space(20)]
    public InputActionReference shootInputAction;
    [Space(10)]
    public InputActionReference gripInputAction;
    public bool holdGrip = false;
    [Space(10)]
    public InputActionReference runInputAction;


    [Space(40)]
    public InputActionReference shootInputActionSim;
    public InputActionReference holdInputActionSim;



    
    private bool shooting = false;
    private void Update()
    {
        if (GameManager.instance.PlayingWithVR())
        {
            shooting = shootInputAction.action.IsPressed(); 
            holdGrip = gripInputAction.action.IsPressed();
        }
        else
        {
            shooting = shootInputActionSim.action.IsPressed();
            holdGrip = holdInputActionSim.action.IsPressed();
        }
    }

    private void FixedUpdate()
    {
        if (shooting) weapon.Shoot();
    }

}

