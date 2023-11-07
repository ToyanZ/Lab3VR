using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public Player player;
    public List<InputActionReference> actionReferences;

    public void Resurrect()
    {
        //player.target. vida maxima
        gameObject.SetActive(true);
    }
}
