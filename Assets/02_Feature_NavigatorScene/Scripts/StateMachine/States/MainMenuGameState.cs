using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuGameState : GameState
{
    public GameObject menuGO;
    
    public override void Enter()
    {
        menuGO.SetActive(true);
    }

    public void TransitionToGame(DataScene dataScene)
    {
        gameManager.NextActiveScene = dataScene.scene;
        gameManager.ChangeState(GetComponent<LoadingLevelGameState>());
    }

    public override void Exit()
    {
        menuGO.SetActive(false);
    }
}