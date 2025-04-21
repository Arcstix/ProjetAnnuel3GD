using System;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    private static readonly int Open = Animator.StringToHash("Open");
    private bool hasOpen = false;
    
    public event Action OnOpen; 
    public bool hasCondition = false;

    public void OpenDoor()
    {
        if (hasOpen) return;
        
        hasOpen = true;
        animator.SetTrigger(Open);
        OnOpen?.Invoke();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (hasOpen) return;
        
        if (other.CompareTag("Player") && !hasCondition)
        {
            hasOpen = true;
            animator.SetTrigger(Open);
            OnOpen?.Invoke();
        }
    }
}
