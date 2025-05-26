using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelInitializationGameState : GameState
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private InputSystemUIInputModule inputModule;
    [SerializeField] private SettingsManager settingsManager;

    private GameObject player;
    public event Action<GameObject> OnPlayerInitialized;
    
    public override void Enter()
    {
        InitPlayer();
        OnPlayerInitialized?.Invoke(player);
        player.GetComponent<InputActionMapManager>().SwitchToPlayer();
        inputModule.actionsAsset = player.GetComponent<PlayerInput>().actions;
        settingsManager.SetCameraManager(player.GetComponent<PlayerCameraManager>());
        
        gameManager.ChangeState(GetComponent<PlayingGameState>());
    }
    
    private void InitPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        GameObject spawn = GameObject.FindGameObjectWithTag("Spawn");

        if (spawn != null && player != null)
        {
            player.transform.position = spawn.transform.position;
            return;
        }
        
        if (spawn != null && player == null)
        {
            player = Instantiate(_playerPrefab, spawn.transform.position, Quaternion.identity);
            return;
        }
        
        
        if(spawn == null && player == null)
        {
            Debug.LogError("Spawner with tag Spawn not found");
        }
    }
}