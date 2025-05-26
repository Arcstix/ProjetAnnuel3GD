using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseGameState : GameState
{
    public GameObject pauseMenuPanel;
    public GameObject levelPanel;
    
    private PlayerInput playerInput;
    private PlayerUIManager uiManager;
    private InputActionMapManager actionMapManager;
    private InputAction exitAction;
    
    public override void Enter()
    {
        levelPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);

        Time.timeScale = 0f;
        
        uiManager = gameManager.PlayerRef.GetComponent<PlayerUIManager>();
        playerInput = gameManager.PlayerRef.GetComponent<PlayerInput>();
        actionMapManager = playerInput.GetComponent<InputActionMapManager>();
        
        uiManager.DisableGameCanvas();
        uiManager.DisableHubCanvas();
        SwitchToUIInput();
        exitAction = playerInput.actions["Exit"];
    }

    public override void Tick()
    {
        if (exitAction.WasPressedThisFrame())
        {
            ReturnToGame();
        }
    }

    private void SwitchToPlayerInput()
    {
        actionMapManager.SwitchToPlayer();
    }
    
    private void SwitchToUIInput()
    {
        actionMapManager.SwitchToUI();
    }

    public void TransitionToSettings()
    {
        gameManager.ChangeState(GetComponent<SettingsGameState>());
    }

    //todo Return to game
    public void ReturnToGame()
    {
        SwitchToPlayerInput();
        gameManager.ChangeState(GetComponent<PlayingGameState>());
    }

    public void ReturnToHub(DataScene dataScene)
    {
        gameManager.NextActiveScene = dataScene.scene;
        gameManager.ChangeState(GetComponent<LoadingLevelGameState>());
    }
    
    //todo Return to menu
    public void ReturnToMenu(DataScene dataScene)
    {
        gameManager.NextActiveScene = dataScene.scene;
        gameManager.ChangeState(GetComponent<ReturnToMenuGameState>());
    }
    
    public override void Exit()
    {
        pauseMenuPanel.SetActive(false);
        //Mettre le jeu en pause = responsabilité de PauseState.
        Time.timeScale = 1f; 
        
        if (SceneManager.GetActiveScene().name == "Hub")
        {
            uiManager.EnableHubCanvas();
        }
        else
        {
            uiManager.EnableGameCanvas();
        }
    }
}