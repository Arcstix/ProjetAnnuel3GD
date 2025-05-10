using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            return;
        }
        
        GameObject spawn = GameObject.FindGameObjectWithTag("Spawn");

        if (spawn != null)
        {
            Instantiate(_playerPrefab, spawn.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Spawner with tag Spawn not found");
        }
    }
}