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

        Transform currentSpawner = spawnerManager.GetCurrentSpawnerPosition();

        if (currentSpawner != null && player != null)
        {
            player.transform.position = currentSpawner.position;
            return;
        }
        
        if (currentSpawner != null && player == null)
        {
            player = Instantiate(_playerPrefab, currentSpawner.position, Quaternion.identity);
        }
    }
}