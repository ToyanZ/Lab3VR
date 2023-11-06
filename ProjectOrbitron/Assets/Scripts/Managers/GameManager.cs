using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { None, MainMenu, Match}
    public static GameManager instance;

    [SerializeField] private GameState state;

    public GameObject deviceSimulator;
    public bool useSimulator;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(!useSimulator) deviceSimulator.SetActive(false);
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.MainMenu:

                break; 
            case GameState.Match:

                break;
        }
    }

    public void SetState(GameState newState)
    {
        state = newState;
    }


}
