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
        gameManager.PlayerRef.GetComponent<PlayerUIManager>().EnableGameCanvas();

        if (SceneManager.GetActiveScene().name != "Hub")
        {
            levelPanel.SetActive(true);
            if (!levelManager.GetStartedState())
            {
                levelManager.StartLevel();
            }
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
    }
}