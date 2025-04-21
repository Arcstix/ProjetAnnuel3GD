using Eflatun.SceneReference;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManagerFSM : MonoBehaviour
{
    private SceneReference currentActiveScene;
    private SceneReference nextActiveScene;

    private GameState currentState;

    public List<SceneReference> chunkList = new List<SceneReference>();

    public SceneReference CurrentActiveScene { get => currentActiveScene; set => currentActiveScene = value; }
    public SceneReference NextActiveScene { get => nextActiveScene; set => nextActiveScene = value; }

    private void Start()
    {
        GameState[] states = GetComponents<GameState>();
        foreach (GameState state in states)
        {
            state.Initialize(this);
        }
        
        //Make persistent (EZ version)
        DontDestroyOnLoad(gameObject);
       
        currentState = GetComponent<MainMenuGameState>();
        currentState.Enter();
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