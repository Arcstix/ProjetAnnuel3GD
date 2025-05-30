using System;
using UnityEngine;

public class CheckpointDetection : MonoBehaviour
{
    [SerializeField] private Transform spawner;
    [SerializeField] private int order;
    
    public event Action OnCheckpointActivated;

    private void Start()
    {
        SpawnerManager spawnerManager = GameManagerSM.Instance.GetComponent<SpawnerManager>();
        spawnerManager.RegisterCheckpoint(spawner, order);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCheckpointActivated?.Invoke();
            // TODO : Spécifie qu'il est le nouveau checkpoint actif

            if (GameManagerSM.Instance != null)
            {
                SpawnerManager spawnerManager = GameManagerSM.Instance.GetComponent<SpawnerManager>();
                if (spawnerManager)
                {
                    spawnerManager.SetNewSpawnerPosition(spawner);
                }
            }
        }
    }
}
