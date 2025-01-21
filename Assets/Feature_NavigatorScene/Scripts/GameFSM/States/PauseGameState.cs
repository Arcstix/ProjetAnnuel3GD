using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGameState : GameState
{
    public GameObject pauseMenuPanel;
    
    public override void Enter()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        DisableInputPlayer();
    }

    private void EnableInputPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerInput>().EnableInput();
    }
    
    private void DisableInputPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerInput>().DisableInput();
    }

    //todo Return to game
    public void ReturnToGame()
    {
        EnableInputPlayer();
        fsm.ChangeState(GetComponent<PlayingGameState>());
    }

    //todo Return to menu
    public void ReturnToMenu(DataScene dataScene)
    {
        fsm.NextActiveScene = dataScene.scene;
        fsm.ChangeState(GetComponent<ReturnToMenuGameState>());
    }
    
    public override void Exit()
    {
        pauseMenuPanel.SetActive(false);
        //Mettre le jeu en pause = responsabilité de PauseState.
        Time.timeScale = 1f; 
    }
}