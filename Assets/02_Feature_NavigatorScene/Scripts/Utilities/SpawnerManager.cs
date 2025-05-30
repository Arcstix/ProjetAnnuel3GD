using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private Dictionary<Transform, int> checkpointOrder = new Dictionary<Transform, int>();
    private List<Transform> checkpointsList = new List<Transform>();
    
    private Transform currentSpawnerPosition;
    private int currentSpawnerIndex = 0;

    public Transform GetCurrentSpawnerPosition()
    {
        return currentSpawnerPosition;
    }
    
    public void SetNewSpawnerPosition(Transform spawnerPosition)
    {
        currentSpawnerPosition = spawnerPosition;
        currentSpawnerIndex = checkpointsList.IndexOf(spawnerPosition);
    }

    public void ClearCheckpoints()
    {
        checkpointOrder.Clear();
        checkpointsList.Clear();
    }

    // Appelé au démarrage ou à l'initialisation
    public void RegisterCheckpoint(Transform checkpoint, int order)
    {
        checkpointOrder[checkpoint] = order;
        checkpointsList = GetOrderedCheckpoints();
    }

    // Retourne la liste triée des checkpoints
    private List<Transform> GetOrderedCheckpoints()
    {
        return checkpointOrder
            .OrderBy(pair => pair.Value)
            .Select(pair => pair.Key)
            .ToList();
    }
    
    private void TeleportToCheckpoint(int index, GameObject player)
    {
        if (checkpointsList == null || index < 0 || index >= checkpointsList.Count)
        {
            Debug.LogWarning("Checkpoint index invalide ou non initialisé.");
            return;
        }

        Transform targetCheckpoint = checkpointsList[index];
        player.transform.position = targetCheckpoint.position;
    }
    
    public void TeleportToNextCheckpoint(GameObject player)
    {
        if (checkpointsList == null || checkpointsList.Count == 0)
            return;

        currentSpawnerIndex = (currentSpawnerIndex + 1) % checkpointsList.Count;
        TeleportToCheckpoint(currentSpawnerIndex, player);
    }

    public void TeleportToPreviousCheckpoint(GameObject player)
    {
        if (checkpointsList == null || checkpointsList.Count == 0)
            return;

        currentSpawnerIndex = (currentSpawnerIndex - 1 + checkpointsList.Count) % checkpointsList.Count;
        TeleportToCheckpoint(currentSpawnerIndex, player);
    }
}
