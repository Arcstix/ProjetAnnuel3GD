using System;
using UnityEngine;

[RequireComponent(typeof(InteractiveTarget))]
public class AntenneInteraction : InteractionSystem
{
    public event Action<bool> OnToolInteraction;
    private InteractiveTarget interactiveTarget;

    private void Awake()
    {
        interactiveTarget = GetComponent<InteractiveTarget>();
    }

    private void Start()
    {
        interactiveTarget.EnableInteraction();
        _isInteractive = true;
    }

    public override void Interact(InteractionSystem otherSystem)
    {
        if (otherSystem.interactorType == InteractorType.Tool)
        {
            //Debug.Log("Antenne Interaction");
            OnToolInteraction?.Invoke(true);
            interactiveTarget.DisableInteraction();
        }
    }

    public override void ExitInteraction(InteractionSystem otherSystem)
    {
        if (otherSystem.interactorType == InteractorType.Tool)
        {
            OnToolInteraction?.Invoke(false);
            interactiveTarget.EnableInteraction();
        }
    }
}
