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
        if (!_stateMachine.AbilityManager.aimBotMode)
        {
            // Aim State active just in 1st person
            if (input.PlayerActions.ShootRecallRight.WasPerformedThisFrame() && reusableData.RightObject == null)
            {
                HandleRightAimState();
                return;
            }

            if (input.PlayerActions.ShootRecallLeft.WasPerformedThisFrame() && reusableData.LeftObject == null)
            {
                HandleLeftAimState();
                return;
            }
        }
        
        // Shoot and Recall State
        if (_stateMachine.AbilityManager.aimBotMode)
        {
            // 3rd person
            if (input.PlayerActions.ShootRecallRight.WasPressedThisFrame())
            {
                HandleRightShootRecall();
                return;
            }
            
            if (input.PlayerActions.ShootRecallLeft.WasPressedThisFrame())
            {
                HandleLeftShootRecall();
                return;
            }
        }
        else
        {
            // 1st person 
            if (input.PlayerActions.ShootRecallRight.WasReleasedThisFrame())
            {
                HandleRightShootRecall();
                return;
            }
            
            if (input.PlayerActions.ShootRecallLeft.WasReleasedThisFrame())
            {
                HandleLeftShootRecall();
                return;
            }
        }
        
        // Attraction State
        if (!_stateMachine.AbilityManager.useStamina)
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
            if (input.PlayerActions.AttractionRight.WasPerformedThisFrame())
            {
                HandleAttraction();
            }

            if (input.PlayerActions.AttractionLeft.WasPerformedThisFrame())
            {
                HandleAttraction();
            }
        }
        
        if (input.PlayerActions.ThrowRight.WasPerformedThisFrame() && targetSystem.rightTargetLaunch != null)
        {
            reusableData.RightThrow = true;
            _stateMachine.ChangeState(_stateMachine.ThrowState);
        }

        if (input.PlayerActions.ThrowLeft.WasPerformedThisFrame() && targetSystem.leftTargetLaunch != null)
        {
            reusableData.LeftThrow = true;
            _stateMachine.ChangeState(_stateMachine.ThrowState);
        }
    }
}
