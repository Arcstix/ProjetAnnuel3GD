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

        if (input.PlayerActions.ThrowRecallRight.WasPerformedThisFrame() && reusableData.RightObject == null)
        {
            HandleRightAimState();
            return;
        }

        if (input.PlayerActions.ThrowRecallLeft.WasPerformedThisFrame() && reusableData.LeftObject == null)
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

        if (input.PlayerActions.AttractionRight.WasPerformedThisFrame() && metricsManager.StaminaRight > metricsManager.CurrentMetrics.StaminaData.MinimumStamina)
        {
            HandleAttraction();
        }

        if (input.PlayerActions.AttractionLeft.WasPerformedThisFrame() && metricsManager.StaminaLeft > metricsManager.CurrentMetrics.StaminaData.MinimumStamina)
        {
            HandleAttraction();
        }
    }
}
