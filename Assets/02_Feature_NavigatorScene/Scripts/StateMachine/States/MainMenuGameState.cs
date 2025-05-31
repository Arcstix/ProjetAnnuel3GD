using Eflatun.SceneReference;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class MainMenuGameState : GameState
{
    public GameObject menuGO;
    [SerializeField] private InputSystemUIInputModule inputModule;
    
    private FMOD.Studio.EventInstance soundInstance;
    
    public override void Enter()
    {
        menuGO.SetActive(true);
        soundInstance = RuntimeManager.CreateInstance("event:/Menu/Musique Menu");
        soundInstance.start();
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
        // Stopper et libérer
        soundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        soundInstance.release();
        menuGO.SetActive(false);
    }
}