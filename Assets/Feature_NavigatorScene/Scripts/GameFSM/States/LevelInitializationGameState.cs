using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelInitializationGameState : GameState
{
    [SerializeField] private GameObject _controllerPrefab;
    [SerializeField] private GameObject _clavierPrefab;

    private bool _isController = false;
    
    public override void Enter()
    {
        InitPlayer();
        fsm.ChangeState(GetComponent<PlayingGameState>());
    }

    public void SetInfo(bool isController)
    {
        _isController = isController;
    }
    
    private void InitPlayer()
    {
        GameObject spawn = GameObject.FindGameObjectWithTag("Spawn");

        if (_isController)
        {
            Instantiate(_controllerPrefab, spawn.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_clavierPrefab, spawn.transform.position, Quaternion.identity);
        }
    }
}