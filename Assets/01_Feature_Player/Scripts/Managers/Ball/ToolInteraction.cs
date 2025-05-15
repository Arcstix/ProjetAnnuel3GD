using System;
using UnityEngine;

public class ToolInteraction : InteractionSystem
{
    public event Action OnPlayerInteract;
    public event Action OnInstantDestruction;
    
    public event Action OnInteractionStart;
    public event Action OnInteractionEnd;

    public bool canInteractWithPlayer = true;

    public override void Interactable(bool isInteractable)
    {
        base.Interactable(isInteractable);
        
        canInteractWithPlayer = isInteractable;
        if (isInteractable)
        {
            OnInteractionStart?.Invoke();
        }
        else
        {
            OnInteractionEnd?.Invoke();
        }
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
            case InteractorType.PulseProjectile:
                GetComponent<ToolManager>().SetLaunch(true);
                canInteractWithPlayer = false;
                return;
            case InteractorType.BrightProjectile:
                GetComponent<ToolManager>().SetLaunch(true);
                canInteractWithPlayer = false;
                return;
        }
    }
}
