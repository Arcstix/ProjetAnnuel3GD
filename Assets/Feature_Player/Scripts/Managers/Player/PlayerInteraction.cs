using System;
using UnityEngine;

public class PlayerInteraction : InteractionSystem
{
    public event Action OnEnemyInteract;
    public event Action OnPlatformInteract;
    public event Action OnToolInteract;
    
    public override void Interact(InteractionSystem otherSystem)
    {
        base.Interact(otherSystem);
        
        if (otherSystem.interactorType == InteractorType.Enemy)
        {
            // In case the Player is coming into the enemy // LOOSE GAME
            OnEnemyInteract?.Invoke();
            return;
        }

        if (otherSystem.interactorType == InteractorType.Platform)
        {
            // In case a Player is coming into the platform
            OnPlatformInteract?.Invoke();
            return;
        }

        if (otherSystem.interactorType == InteractorType.Tool)
        {
            // In case the Player trigger into the tool // DESTROY TOOL
            OnToolInteract?.Invoke();
            GetComponent<PlayerMetricsManager>().AddExternForce(1f);
            Destroy(otherSystem.gameObject);
            return;
        }
    }
}
