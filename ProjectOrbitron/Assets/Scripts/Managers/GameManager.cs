using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { None, MainMenu, Match}
    public enum InputMode { Keyboard, VR}
    public static GameManager instance;

    [SerializeField] private GameState state;

    [Space(10)]
    [SerializeField] private InputMode inputMode = InputMode.Keyboard;
    [SerializeField] private GameObject deviceSimulator;
    public Player player;
    public float enemyPauseTime = 1.5f;
    public float enemyAttackDamage = 0.7f;

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
        if(inputMode == InputMode.VR) deviceSimulator.SetActive(false);
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

    public bool PlayingWithVR()
    {
        return inputMode == InputMode.VR;
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
}
