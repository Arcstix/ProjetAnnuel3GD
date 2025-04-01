using System;
using UnityEngine;

public class ToolInteraction : InteractionSystem
{
    public event Action OnPlayerInteract;
    public event Action OnInstantDestruction;

    public bool canInteractWithPlayer = true;

    public override void Interactable(bool isInteractable)
    {
        base.Interactable(isInteractable);
        
        canInteractWithPlayer = isInteractable;
    }

    public override void Interact(InteractionSystem otherSystem)
    {
        base.Interact(otherSystem);
        
        switch (otherSystem.interactorType)
        {
            case InteractorType.Player:
                if (canInteractWithPlayer)
                {
                    OnPlayerInteract?.Invoke();
                    GetComponentInParent<InteractiveTarget>().EnableInteraction();
                    Destroy(this.gameObject);
                }
                return;
            case InteractorType.InstantDestructible:
                // In case a tool object trigger into an instant destructible
                OnInstantDestruction?.Invoke();
                Destroy(otherSystem.gameObject);
                Destroy(this.gameObject);
                return;
            case InteractorType.Projectile:
                GetComponent<ToolManager>().SetLaunch(true);
                canInteractWithPlayer = false;
                return;
        }
    }
}
