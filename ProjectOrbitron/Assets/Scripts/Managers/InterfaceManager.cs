using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InterfaceManager : MonoBehaviour
{
    public RectTransform pauseMenu;
    public InputActionReference pauseInputAction;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pauseInputAction.action.WasPressedThisFrame()) pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
    }
}
