using System;
using Unity.VisualScripting;
using UnityEngine;

public class DestructibleWallInteraction : InteractionSystem
{
    
    public event Action OnEnemyInteract;
    public event Action OnWallDestroy;
    
    public override void Interact(InteractionSystem otherSystem)
    {
        base.Interact(otherSystem);
        
        if (otherSystem.interactorType == InteractorType.Enemy)
        {
            // In case the Projectile is coming into the enemy // add rigidbody to enemy
            OnEnemyInteract?.Invoke();
            Destroy(this.gameObject);
            return;
        }

        if (otherSystem.interactorType == InteractorType.Projectile)
        {
            OnWallDestroy?.Invoke();
            Destroy(this.gameObject);
            return;
        }
    }
}
