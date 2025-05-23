using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class MainMenuGameState : GameState
{
    public GameObject menuGO;
    [SerializeField] private InputSystemUIInputModule inputModule;
    
    public override void Enter()
    {
        menuGO.SetActive(true);
        inputModule.actionsAsset.Enable();
    }

    public void TransitionToSettings()
    {
        gameManager.ChangeState(GetComponent<SettingsGameState>());
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