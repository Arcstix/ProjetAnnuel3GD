using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGameState : GameState
{
    public GameObject pauseMenuPanel;
    
    public override void Enter()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        DisableInputPlayer();
    }

    private void EnableInputPlayer()
    {
        gameManager.PlayerRef.GetComponent<PlayerInput>().enabled = true;
    }
    
    private void DisableInputPlayer()
    {
        gameManager.PlayerRef.GetComponent<PlayerInput>().enabled = false;
    }

    //todo Return to game
    public void ReturnToGame()
    {
        EnableInputPlayer();
        gameManager.ChangeState(GetComponent<PlayingGameState>());
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
    }
}