using System;
using UnityEngine;
using UnityEngine.Events;

public class BoosterDetection : MonoBehaviour
{
    [SerializeField] private float boosterSpeed = 0.2f;
    
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
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.AddForce(rb.velocity * boosterSpeed, ForceMode.Impulse);
    }
}
