using System;
using UnityEngine;

public class ToolInteraction : InteractionSystem
{
    public event Action OnPlayerInteract;
    public event Action OnPlatformInteract;
    public event Action OnToolInteract;
    public event Action OnInstantDestruction;
    
    public override void Interact(InteractionSystem otherSystem)
    {
        base.Interact(otherSystem);
        
        switch (otherSystem.interactorType)
        {
            case InteractorType.Player:
                // The tool is coming on the player
                OnPlayerInteract?.Invoke();
                Destroy(this.gameObject);
                return;
            case InteractorType.InstantDestructible:
                // In case a tool object trigger into an instant destructible
                OnInstantDestruction?.Invoke();
                Destroy(this.gameObject, 0.01f);
                return;
            case InteractorType.Projectile:
                GetComponent<ToolManager>().Launch();
                return;
        }
    }
}
