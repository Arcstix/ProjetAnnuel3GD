using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileInteraction : InteractionSystem
{
    public event Action OnPlatformInteract;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        isInteractive = true;
    }

    public override void CollisionInteract(InteractionSystem otherSystem, Collision other)
    {
        base.CollisionInteract(otherSystem, other);
        
        Debug.Log("Interact");
        
        if (otherSystem.interactorType == InteractorType.Platform)
        {
            // the projectile stuck into the platform and interaction is stopped
            Debug.Log(otherSystem.interactorType);
            OnPlatformInteract?.Invoke();
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            isInteractive = false;
            GetComponent<InteractiveTarget>().DisableInteraction();
            return;
        }
    }

    public override void Interact(InteractionSystem otherSystem)
    {
        
    }
}
