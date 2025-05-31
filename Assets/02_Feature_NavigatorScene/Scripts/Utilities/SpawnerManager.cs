using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private Dictionary<Vector3, int> checkpointOrder = new Dictionary<Vector3, int>();
    private List<Vector3> checkpointsList = new List<Vector3>();
    
    private Vector3 currentSpawnerPosition = Vector3.zero;
    private int currentSpawnerIndex = 0;

    public Vector3 GetCurrentSpawnerPosition()
    {
        return currentSpawnerPosition;
    }
    
    public void SetNewSpawnerPosition(Vector3 spawnerPosition)
    {
        currentSpawnerPosition = spawnerPosition;
        currentSpawnerIndex = checkpointsList.IndexOf(spawnerPosition);
    }

    public void ClearCheckpoints()
    {
        checkpointOrder.Clear();
        checkpointsList.Clear();
        currentSpawnerIndex = 0;
        currentSpawnerPosition = Vector3.zero;
    }

    // Appelé au démarrage ou à l'initialisation
    public void RegisterCheckpoint(Vector3 checkpoint, int order)
    {
        checkpointOrder[checkpoint] = order;
        checkpointsList = GetOrderedCheckpoints();
    }

    // Retourne la liste triée des checkpoints
    private List<Vector3> GetOrderedCheckpoints()
    {
        return checkpointOrder
            .OrderBy(pair => pair.Value)
            .Select(pair => pair.Key)
            .ToList();
    }

    public void TeleportToCurrentIndex(GameObject player)
    {
        if (checkpointsList == null || currentSpawnerIndex < 0 || currentSpawnerIndex >= checkpointsList.Count)
        {
            Debug.LogWarning("Checkpoint index invalide ou non initialisé.");
            return;
        }

        Vector3 targetCheckpoint = checkpointsList[currentSpawnerIndex];
        player.transform.position = targetCheckpoint;
    }
    
    private void TeleportToCheckpoint(int index, GameObject player)
    {
        if (checkpointsList == null || index < 0 || index >= checkpointsList.Count)
        {
            Debug.LogWarning("Checkpoint index invalide ou non initialisé.");
            return;
        }

        Vector3 targetCheckpoint = checkpointsList[index];
        player.transform.position = targetCheckpoint;
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
