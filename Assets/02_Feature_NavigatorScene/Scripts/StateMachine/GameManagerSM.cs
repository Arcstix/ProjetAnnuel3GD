using Eflatun.SceneReference;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManagerSM : MonoBehaviour
{
    private SceneReference lastActiveScene;
    private SceneReference currentActiveScene;
    private SceneReference nextActiveScene;
    
    private LevelInitializationGameState initializationGameState;
    
    private GameObject playerRef;
    
    [SerializeField] private SceneReference currentScene;
    [SerializeField] private GameState defaultGameState;
    [SerializeField] private GameState currentState;

    public List<SceneReference> chunkList = new List<SceneReference>();
    
    public SceneReference LastActiveScene { get => lastActiveScene; set => lastActiveScene = value; }
    public SceneReference CurrentActiveScene { get => currentActiveScene; set => currentActiveScene = value; }
    public SceneReference NextActiveScene { get => nextActiveScene; set => nextActiveScene = value; }
    
    public GameObject PlayerRef { get => playerRef; set => playerRef = value; }
    
    public static GameManagerSM Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Évite les doublons
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persiste entre les scènes
        initializationGameState = GetComponent<LevelInitializationGameState>();
    }
    
    private void Start()
    {
        initializationGameState.OnPlayerInitialized += UpdatePlayerRef;
        GameState[] states = GetComponents<GameState>();
        foreach (GameState state in states)
        {
            state.Initialize(this);
        }

        if (defaultGameState != null)
        {
            currentState = defaultGameState;
        }
        else
        {
            currentState = GetComponent<MainMenuGameState>();
        }

        if (currentScene != null)
        {
            currentActiveScene = currentScene;
        }
        currentState.Enter();
    }

    private void UpdatePlayerRef(GameObject newPlayerRef)
    {
        playerRef = newPlayerRef;
    }

    public void AddChunk(SceneReference scene)
    {
        chunkList.Add(scene);
    }

    public void RemoveChunk(SceneReference scene)
    {
        chunkList.Remove(scene);
    }

    private void Update()
    {
        currentState?.Tick();
    }

    public void ChangeState(GameState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}