using System;
using UnityEngine;

public class DestructibleInteraction : InteractionSystem
{
    public event Action OnPlayerInteract;
    public event Action OnPlatformInteract;
    public event Action OnToolInteract;

    public override void Interact(InteractionSystem otherSystem)
    {
        base.Interact(otherSystem);
        
        if (otherSystem.interactorType == InteractorType.Player)
        {
            // In case the destructible object is coming into the enemy // Destroy object
            OnPlayerInteract?.Invoke();
            Destroy(this.gameObject);
            return;
        }

        if (otherSystem.interactorType == InteractorType.PulseProjectile)
        {
            // In case a destructible object is coming into the platform // Destroy object
            OnPlatformInteract?.Invoke();
            Destroy(this.gameObject);
            return;
        }
    }
}
