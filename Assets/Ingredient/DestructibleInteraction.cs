using System;
using UnityEngine;

public class DestructibleInteraction : InteractionSystem
{
    public event Action OnEnemyInteract;
    public event Action OnPlatformInteract;
    public event Action OnToolInteract;

    public override void Interact(InteractionSystem otherSystem)
    {
        base.Interact(otherSystem);
        
        if (otherSystem.interactorType == InteractorType.Enemy)
        {
            // In case the destructible object is coming into the enemy // Destroy object
            OnEnemyInteract?.Invoke();
            Destroy(this.gameObject);
            return;
        }

        if (otherSystem.interactorType == InteractorType.Projectile)
        {
            // In case a destructible object is coming into the platform // Destroy object
            OnPlatformInteract?.Invoke();
            Destroy(this.gameObject);
            return;
        }

        if (interactorType == InteractorType.Tool)
        {
            // In case the destructible object trigger into the tool // DESTROY object and tool
            OnToolInteract?.Invoke();
            Destroy(this.gameObject, 0.01f);
            return;
        }
    }
}
