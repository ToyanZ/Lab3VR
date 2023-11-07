using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Weapon weapon;
    public InputActionReference shootInputActionSim;
    public InputActionReference shootInputAction;

    [Space(20)]
    public InputActionReference pauseInputActionSim;
    public InputActionReference pauseInputAction;



    void Update()
    {
        if(!GameManager.instance.useSimulator)
        {
            if (pauseInputAction.action.WasPressedThisFrame()) InterfaceManager.instance.PauseMenu();
            if (shootInputAction.action.WasPressedThisFrame()) weapon.Shoot();
        }
        else
        {
            if (pauseInputActionSim.action.WasPressedThisFrame()) InterfaceManager.instance.PauseMenu();
            if (shootInputActionSim.action.WasPressedThisFrame()) weapon.Shoot();
        }
    }
}
