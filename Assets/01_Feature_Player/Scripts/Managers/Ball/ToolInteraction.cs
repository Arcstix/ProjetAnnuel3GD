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
                    InteractiveTarget interactiveTarget = GetComponentInParent<InteractiveTarget>();
                    if (interactiveTarget)
                    {
                        interactiveTarget.EnableInteraction();
                    }
                    ToolManager toolManager = GetComponentInParent<ToolManager>();
                    
                    toolManager.DisableInteraction();
                    
                    Destroy(this.gameObject);
                }
                return;
            case InteractorType.Projectile:
                GetComponent<ToolManager>().SetLaunch(true);
                canInteractWithPlayer = false;
                return;
        }
    }
}
