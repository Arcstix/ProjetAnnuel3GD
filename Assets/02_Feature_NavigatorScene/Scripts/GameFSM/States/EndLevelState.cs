using UnityEngine;
using UnityEngine.InputSystem;
public class EndLevelState : GameState
{
    public GameObject endGameStats;

    public override void Enter()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        DisableInputPlayer();
    }
    public override void Tick()
    {
        //animation unity event ? 
        //fin anim = affiche écran stats
        endGameStats.SetActive(true);
        fsm.ChangeState(GetComponent<ReturnToMenuGameState>());
    }
    public override void Exit()
    {
        
    }
    public void ReturnToMenu(DataScene dataScene)
    {
        fsm.NextActiveScene = dataScene.scene;
        fsm.ChangeState(GetComponent<ReturnToMenuGameState>());
    }
    //j'ai mis l'état d'initialisation de la scène mais je sais pas si ça va recharger correctement la scène
    public void ReturnToStartLevel(DataScene dataScene)
    {
        fsm.ChangeState(GetComponent<LevelInitializationGameState>());
    }
    private void DisableInputPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerInput>().enabled = false; //pas sur que la ref des inputs soit ok
    }
}
