using System;
using UnityEngine;

public class InteractionSystem : MonoBehaviour, I_Interact
{
    public InteractorType interactorType = InteractorType.None;
    public bool isInteractive = false;
    public bool isInteracting = false;

    public void Interactable(bool isInteractable)
    {
        this.isInteractive = isInteractable;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!isInteractive) return;
        if (isInteracting) return;
        
        if (other.gameObject.TryGetComponent(out InteractionSystem interact))
        {
            Interact(interact);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isInteracting)
        {
            isInteracting = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (isInteracting)
        {
            isInteracting = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isInteractive) return;
        if (isInteracting) return;
        
        if (other.gameObject.TryGetComponent(out InteractionSystem interact))
        {
            CollisionInteract(interact, other);
        }
    }

    public virtual void Interact(InteractionSystem otherSystem)
    {
        isInteracting = true;
    }

    public virtual void CollisionInteract(InteractionSystem otherSystem, Collision otherCollision)
    {
        isInteracting = true;
    }
}
