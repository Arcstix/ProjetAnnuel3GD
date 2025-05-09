using UnityEngine;
using UnityEngine.Events;
public class TriggerEventCall : MonoBehaviour
{
    public UnityEvent onTriggerEnter; // Assigné dynamiquement

    private OnBoardingUI onboarding;

    void Start()
    {
      
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ou autre condition
        {
            onTriggerEnter?.Invoke();
        }
    }
}

