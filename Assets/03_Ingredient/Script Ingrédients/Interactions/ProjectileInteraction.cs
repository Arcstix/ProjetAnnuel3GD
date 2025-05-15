using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(LandingPlatform), typeof(InteractiveTarget))]
public class ProjectileInteraction : InteractionSystem
{
    public event Action OnPlatformInteract;
    public event Action OnToolInteract;
    public event Action OnExitToolInteract;

    private Rigidbody rb;
    private InteractiveTarget _target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    protected virtual void Start()
    {
        _target = GetComponent<InteractiveTarget>();
        _target.SetInteractionType(interactorType);
        _isInteractive = true;
    }

    public override void Interact(InteractionSystem otherSystem)
    {
        if (otherSystem.interactorType == InteractorType.Platform)
        {
            // the projectile stuck into the platform and became a point of attachment
            OnPlatformInteract?.Invoke();
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<AnchorInteraction>().enabled = true;
            GetComponent<LandingPlatform>().SetAvailableState(true);
            if (!GetComponent<InteractiveTarget>().isAvailable)
            {
                PlayerAbilityManager abilityManager = _target.GetPlayer().GetComponent<PlayerAbilityManager>();
                abilityManager.AbilityStateMachine?.ChangeState(abilityManager.AbilityStateMachine.RecallAllState);
            }
            
            this.enabled = false;
            return;
        }
        
        if (otherSystem.interactorType == InteractorType.Tool)
        {
            // the projectile is no more targetable
            OnToolInteract?.Invoke();
            Debug.Log("Interact Performed");
            GetComponent<InteractiveTarget>().DisableInteraction();
            _isInteractive = false;
        }
    }

    public override void ExitInteraction(InteractionSystem otherSystem)
    {
        if (otherSystem.interactorType == InteractorType.Tool)
        {
            // the projectile is no more targetable
            OnExitToolInteract?.Invoke();
            GetComponent<InteractiveTarget>().EnableInteraction();
            _isInteractive = true;
        }
    }
}
