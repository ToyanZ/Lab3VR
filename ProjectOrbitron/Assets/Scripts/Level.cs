using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameManager.GameState type;
    private void Start()
    {
        GameManager.instance.SetState(type);
    }
}
