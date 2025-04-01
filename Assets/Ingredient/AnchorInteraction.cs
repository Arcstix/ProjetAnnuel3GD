using System;
using UnityEngine;

[RequireComponent(typeof(InteractiveTarget))]
public class AnchorInteraction : InteractionSystem
{
    public event Action OnToolInteract;
    
    private InteractiveTarget _target;
    
    private void Start()
    {
        _target = GetComponent<InteractiveTarget>();
        _target.SetInteractionType(interactorType);
        _isInteractive = true;
        
        LandingPlatform landingPlatform = GetComponent<LandingPlatform>();
        if (landingPlatform != null)
        {
            landingPlatform.SetLandingState(true);
        }
    }

    public override void Interact(InteractionSystem otherSystem)
    {
        if (otherSystem.interactorType == InteractorType.Wall)
        {
            _onWall = true;
        }
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
