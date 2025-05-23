using UnityEngine;
using UnityEngine.InputSystem;

public class PlayingGameState : GameState
{
    private PlayerInput playerInput;
    private PlayerMovementManager playerMovement;
    private InputActionMapManager actionMapManager;
    private InputAction menuAction;
    
    public override void Enter()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        playerInput = gameManager.PlayerRef.GetComponent<PlayerInput>();
        playerMovement = playerInput.GetComponent<PlayerMovementManager>();
        actionMapManager = playerInput.GetComponent<InputActionMapManager>();
        menuAction = playerInput.actions["Menu"];
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