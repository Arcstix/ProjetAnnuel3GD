using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInitializationGameState : GameState
{
    [SerializeField] private GameObject _playerPrefab;

    public override void Enter()
    {
        InitPlayer();
        fsm.ChangeState(GetComponent<PlayingGameState>());
    }
    
    private void InitPlayer()
    {
        GameObject spawn = GameObject.FindGameObjectWithTag("Spawn");

        Instantiate(_playerPrefab, spawn.transform.position, Quaternion.identity);
    }
}