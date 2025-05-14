using UnityEngine;
using UnityEngine.Events;
public class TriggerEventCall : MonoBehaviour
{
    public UnityEvent onTriggerEnter; // Assigné dynamiquement
    public UnityEvent onTriggerExit;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ou autre condition
        {
            onTriggerEnter?.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onTriggerExit?.Invoke();
        }
    }
}

