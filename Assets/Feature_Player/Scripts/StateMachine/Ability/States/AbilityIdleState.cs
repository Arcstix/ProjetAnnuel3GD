using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityIdleState : AbilityState
{
    private readonly MeshRenderer leftTool;
    private readonly MeshRenderer rightTool;
    
    public AbilityIdleState(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
        leftTool = leftLauncher.GetComponent<MeshRenderer>();
        rightTool = rightLauncher.GetComponent<MeshRenderer>();
    }

    public override void Tick()
    {
        base.Tick();

        bool rightToolEnabled = reusableData.RightObject == null && !rightTool.enabled;
        if (rightToolEnabled)
        {
            rightTool.enabled = true;
        }
        
        bool leftToolEnabled = reusableData.LeftObject == null && !leftTool.enabled;
        if (leftToolEnabled)
        {
            leftTool.enabled = true;
        }
    }

    public override void HandleInput()
    {
        base.HandleInput();
        
        // Aim State
        if (!_stateMachine.AbilityManager.thirdPersonMode)
        {
            // Aim State active just in 1st person
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
        }
        
        // Shoot and Recall State
        if (_stateMachine.AbilityManager.thirdPersonMode)
        {
            // 3rd person
            if (input.PlayerActions.ThrowRecallRight.WasPressedThisFrame())
            {
                HandleRightShootRecall();
                return;
            }
            
            if (input.PlayerActions.ThrowRecallLeft.WasPressedThisFrame())
            {
                HandleLeftShootRecall();
                return;
            }
        }
        else
        {
            // 1st person 
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
        }
        
        // Attraction State
        if (_stateMachine.AbilityManager.thirdPersonMode)
        {
            // 3rd Person
            if (input.PlayerActions.AttractionRight.WasPerformedThisFrame())
            {
                HandleAttraction();
            }

            if (input.PlayerActions.AttractionLeft.WasPerformedThisFrame())
            {
                HandleAttraction();
            }
        }
        else
        {
            // 1st Person
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
}
