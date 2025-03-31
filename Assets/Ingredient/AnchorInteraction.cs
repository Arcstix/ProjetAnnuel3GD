using System;
using UnityEngine;

public class AnchorInteraction : InteractionSystem
{
    public event Action OnToolInteract;
    
    protected override void Start()
    {
        base.Start();
        _isInteractive = true;
        
        GetComponent<LandingPlatform>().SetLandingState(true);
    }

    public override void Interact(InteractionSystem otherSystem)
    {
        
    }

    public override void ExitInteraction(InteractionSystem otherSystem)
    {
        // if (otherSystem.interactorType == InteractorType.Platform)
        // {
        //     // the projectile is no more targetable
        //     OnToolInteract?.Invoke();
        //     GetComponent<ProjectileInteraction>().enabled = true;
        //     this.enabled = false;
        // }

        if (otherSystem.interactorType == InteractorType.Tool)
        {
            GetComponent<InteractiveTarget>().EnableInteraction();
        }
    }
}
