using UnityEngine;
using UnityEngine.InputSystem.UI;

public class SettingsGameState : GameState
{
    [SerializeField] private SettingsManager settingsManager;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private InputSystemUIInputModule inputModule;
    
    public override void Enter()
    {
        settingsPanel.SetActive(true);
        settingsManager.SetAllSettings();

        if (gameManager.PlayerRef)
        {
            gameManager.PlayerRef.GetComponent<PlayerCameraManager>().UnsubscribeFreeLookAction();
        }
    }

    public override void Tick()
    {
        base.Tick();

        if (inputModule.cancel.action.WasPressedThisFrame())
        {
            if (gameManager.LastGameState != null)
            {
                if (gameManager.LastGameState == GetComponent<MainMenuGameState>())
                {
                    ReturnToMainMenu();
                }

                if (gameManager.LastGameState == GetComponent<PauseGameState>())
                {
                    ReturnToPause();
                }
            }
            else
            {
                ReturnToMainMenu();
            }
        }
    }

    private void ReturnToMainMenu()
    {
        gameManager.ChangeState(GetComponent<MainMenuGameState>());
    }

    private void ReturnToPause()
    {
        gameManager.ChangeState(GetComponent<PauseGameState>());
    }

    public override void Exit()
    {
        settingsPanel.SetActive(false);
        
        if (gameManager.PlayerRef)
        {
            gameManager.PlayerRef.GetComponent<PlayerCameraManager>().SubscribeFreeLookAction();
        }
    }
}
