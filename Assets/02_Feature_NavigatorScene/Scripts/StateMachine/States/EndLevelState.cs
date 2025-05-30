using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class EndLevelState : GameState
{
    [SerializeField] private GameObject levelPanel;
    public GameObject endLevelUI;
    
    private LevelManager levelManager;
    private SpawnerManager spawnerManager;

    private void Awake()
    {
        levelManager = GetComponent<LevelManager>();
        spawnerManager = GetComponent<SpawnerManager>();
    }

    public override void Enter()
    {
        levelManager.EndLevel();
        levelPanel.SetActive(false);
        endLevelUI.SetActive(true);
        spawnerManager.ClearCheckpoints();
        gameManager.PlayerRef.GetComponent<PlayerUIManager>().DisableGameCanvas();
        InputActionMapManager mapManager = GameManagerSM.Instance.PlayerRef.GetComponent<InputActionMapManager>();
        mapManager.SwitchToUI();
    }
    
    public override void Tick()
    {
        
    }
    
    public override void Exit()
    {
        endLevelUI.SetActive(false);
    }
    
    public void ReturnToMenu(DataScene dataScene)
    {
        gameManager.NextActiveScene = dataScene.scene;
        gameManager.ChangeState(GetComponent<ReturnToMenuGameState>());
    }
    
    //j'ai mis l'état d'initialisation de la scène mais je sais pas si ça va recharger correctement la scène
    public void ReturnToStartLevel(DataScene dataScene)
    {
        gameManager.NextActiveScene = dataScene.scene;
        gameManager.ChangeState(GetComponent<LoadingLevelGameState>());
    }
}
