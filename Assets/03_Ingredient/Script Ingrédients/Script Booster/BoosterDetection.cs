using System;
using UnityEngine;
using UnityEngine.Events;

public class BoosterDetection : MonoBehaviour
{
    [Header("Enter Detection")]
    public UnityEvent<GameObject> OnBoosterDetected;
    
    [Header("Exit Detection")]
    public UnityEvent OnBoosterExited;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnBoosterDetected?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnBoosterExited?.Invoke();
        }
    }

    public void BoostPlayer(GameObject player)
    {
        player.GetComponent<PlayerMetricsManager>().AddExternForce(1);
    }
}
