using System;
using UnityEngine;

public class ToolInteraction : InteractionSystem
{
    public event Action OnPlayerInteract;
    public event Action OnEnemyInteract;
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
            case InteractorType.Enemy:
                // In case the tool is coming into the enemy
                OnEnemyInteract?.Invoke();
                Destroy(this.gameObject);
                return;
            case InteractorType.Platform:
                // In case a tool is coming into the platform
                OnPlatformInteract?.Invoke();
                Destroy(this.gameObject);
                return;
            case InteractorType.Tool:
                // In case the tool trigger into another tool
                OnToolInteract?.Invoke();
                Destroy(otherSystem.gameObject);
                Destroy(this.gameObject);
                return;
            case InteractorType.InstantDestructible:
                // In case a tool object trigger into an instant destructible// IMPOSSIBLE CASE
                OnInstantDestruction?.Invoke();
                Destroy(otherSystem.gameObject);
                Destroy(this.gameObject);
                return;
        }
    }
}
