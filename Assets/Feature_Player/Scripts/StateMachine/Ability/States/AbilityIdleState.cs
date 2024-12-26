using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityIdleState : AbilityState
{
    public AbilityIdleState(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (input.PlayerActions.ThrowRecallRight.WasPerformedThisFrame())
        {
            HandleRightAimState();
            return;
        }

        if (input.PlayerActions.ThrowRecallLeft.WasPerformedThisFrame())
        {
            HandleLeftAimState();
            return;
        }

        if (input.PlayerActions.ThrowRecallRight.WasReleasedThisFrame())
        {
            HandleRightShootRecall();
            return;
        }

        if (input.PlayerActions.ThrowRecallLeft.WasReleasedThisFrame())
        {
            HandleLeftShootRecall();
            return;
        }

        if (input.PlayerActions.AttractionRight.WasPerformedThisFrame())
        {
            HandleRightAttraction();
            return;
        }

        if (input.PlayerActions.AttractionLeft.WasPerformedThisFrame())
        {
            HandleLeftAttraction();
        }
    }
}
