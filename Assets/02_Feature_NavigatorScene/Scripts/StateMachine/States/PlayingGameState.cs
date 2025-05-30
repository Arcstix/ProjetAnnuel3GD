using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[RequireComponent(typeof(LevelManager))]
public class PlayingGameState : GameState
{
    [SerializeField] private GameObject levelPanel;
    
    private LevelManager levelManager;
    private PlayerInput playerInput;
    private PlayerMovementManager playerMovement;
    private InputActionMapManager actionMapManager;
    private InputAction menuAction;
    private InputAction nextSpawner;
    private InputAction previousSpawner;

    private void Awake()
    {
        levelManager = GetComponent<LevelManager>();
    }

    public override void Enter()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        playerInput = gameManager.PlayerRef.GetComponent<PlayerInput>();
        playerMovement = playerInput.GetComponent<PlayerMovementManager>();
        actionMapManager = playerInput.GetComponent<InputActionMapManager>();
        menuAction = playerInput.actions["Menu"];
        nextSpawner = playerInput.actions["NextSpawner"];
        previousSpawner = playerInput.actions["PreviousSpawner"];

        PlayerUIManager playerUI = gameManager.PlayerRef.GetComponent<PlayerUIManager>();
        
        if (SceneManager.GetActiveScene().name != "Hub")
        {
            playerUI.EnableGameCanvas();
            playerUI.DisableHubCanvas();
            
            levelPanel.SetActive(true);
            if (!levelManager.GetStartedState())
            {
                levelManager.StartLevel();
            }
        }
        else
        {
            playerUI.DisableGameCanvas();
            playerUI.EnableHubCanvas();
        }
    }

    public override void Tick()
    {
        //Todo Pause game when pressing escape

        if (playerMovement.ReusableData == null) return;
        
        if(actionMapManager.IsInActionMap("Player") && !playerMovement.ReusableData.InAir)
        {
            if (menuAction.WasPressedThisFrame())
            {
                gameManager.ChangeState(GetComponent<PauseGameState>());
            }
        }

        if (nextSpawner.WasPerformedThisFrame())
        {
            GetComponent<SpawnerManager>().TeleportToNextCheckpoint(gameManager.PlayerRef);
        }

        if (previousSpawner.WasPerformedThisFrame())
        {
            GetComponent<SpawnerManager>().TeleportToPreviousCheckpoint(gameManager.PlayerRef);
        }
    }

    public void ReloadLevel()
    {
        gameManager.ChangeState(GetComponent<ReloadingLevelGameState>());
    }
}