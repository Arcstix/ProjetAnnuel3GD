using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyInteraction : InteractionSystem
{
    public event Action OnEnemyInteract;
    public event Action OnPlatformInteract;
    public event Action OnPlayerInteract;
    public event Action OnWallDestroy;
    
    public override void Interact(InteractionSystem otherSystem)
    {
        base.Interact(otherSystem);
        
        if (otherSystem.interactorType == InteractorType.Enemy)
        {
            // In case the Enemy is coming into the enemy // LOOSE GAME
            OnEnemyInteract?.Invoke();
            if (!otherSystem.TryGetComponent(out Rigidbody otherRigidbody))
            {
                otherSystem.AddComponent<Rigidbody>();
            }

            if (!TryGetComponent(out Rigidbody rigidbody))
            {
                gameObject.AddComponent<Rigidbody>();
            }
            return;
        }
        
        if (otherSystem.interactorType == InteractorType.DestructibleWall)
        {
            OnWallDestroy?.Invoke();
            Destroy(otherSystem.gameObject);
        }

        if (otherSystem.interactorType == InteractorType.Projectile)
        {
            if (!otherSystem.TryGetComponent(out Rigidbody otherRigidbody))
            {
                otherSystem.AddComponent<Rigidbody>();
            }
        }

        if (otherSystem.interactorType == InteractorType.Platform)
        {
            // In case a Player is coming into the platform
            OnPlatformInteract?.Invoke();
            return;
        }

        if (otherSystem.interactorType == InteractorType.Player)
        {
            // In case the Enemy trigger into the Player // DESTROY Enemy
            OnPlayerInteract?.Invoke();
            Destroy(this.gameObject);
            return;
        }
    }
}
