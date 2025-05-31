using System;
using System.Collections.Generic;
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


    private SpawnerManager spawnerManager;
    
    public event Action<GameObject> OnPlayerInitialized;

    private void Awake()
    {
        spawnerManager = GetComponent<SpawnerManager>();
    }

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
        
        if (SceneManager.GetActiveScene().name != "Hub")
        {
            Vector3 currentSpawner = spawnerManager.GetCurrentSpawnerPosition();
        
            if (currentSpawner == Vector3.zero && player != null)
            {
                spawnerManager.TeleportToCurrentIndex(player);
                return;
            }
        
            if (currentSpawner != Vector3.zero && player != null)
            {
                player.transform.position = currentSpawner;
                return;
            }
        
            if (currentSpawner != Vector3.zero && player == null)
            {
                player = Instantiate(_playerPrefab, currentSpawner, Quaternion.identity);
            }
        }
    }
}