using System;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileInteraction : InteractionSystem
{
    public event Action OnEnemyInteract;
    public event Action OnPlatformInteract;
    public event Action OnToolInteract;
    public event Action OnWallDestroy;
    
    public override void Interact(InteractionSystem otherSystem)
    {
        base.Interact(otherSystem);
        
        if (otherSystem.interactorType == InteractorType.Enemy)
        {
            // In case the Projectile is coming into the enemy // add rigidbody to enemy
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
        
        if (otherSystem.interactorType == InteractorType.Projectile)
        {
            if (!otherSystem.TryGetComponent(out Rigidbody otherRigidbody))
            {
                otherSystem.AddComponent<Rigidbody>();
            }
            
            if (!TryGetComponent(out Rigidbody rigidbody))
            {
                gameObject.AddComponent<Rigidbody>();
            }
        }

        if (otherSystem.interactorType == InteractorType.Platform)
        {
            // In case a projectile is coming into the platform
            OnPlatformInteract?.Invoke();
            return;
        }

        if (otherSystem.interactorType == InteractorType.DestructibleWall)
        {
            OnWallDestroy?.Invoke();
            Destroy(otherSystem.gameObject);
        }

        if (otherSystem.interactorType == InteractorType.Tool)
        {
            // In case the Projectile trigger into the tool // DESTROY TOOL
            OnToolInteract?.Invoke();
            return;
        }
    }
}
