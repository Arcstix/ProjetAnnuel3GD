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
        if (_stateMachine.AbilityManager.kinichMode)
        {
            // Aim State active just in 1st person
            if (rightShootRecall.WasPerformedThisFrame() && reusableData.RightObject == null)
            {
                HandleRightAimState();
                return;
            }

            if (leftShootRecall.WasPerformedThisFrame() && reusableData.LeftObject == null)
            {
                HandleLeftAimState();
                return;
            }
        }
        
        // Shoot and Recall State
        if (!_stateMachine.AbilityManager.kinichMode)
        {
            // Pressed this frame
            if (rightShootRecall.WasPressedThisFrame())
            {
                HandleRightShootRecall();
                return;
            }
            
            if (leftShootRecall.WasPressedThisFrame())
            {
                HandleLeftShootRecall();
                return;
            }
        }
        else
        {
            // Released this frame 
            if (rightShootRecall.WasReleasedThisFrame())
            {
                HandleRightShootRecall();
                return;
            }
            
            if (leftShootRecall.WasReleasedThisFrame())
            {
                HandleLeftShootRecall();
                return;
            }
        }
        
        //Attraction State
        if (rightAttractionThrow.WasPerformedThisFrame())
        {
            HandleRightAttractionThrow();
        }

        if (leftAttractionThrow.WasPerformedThisFrame())
        {
            HandleLeftAttractionThrow();
        }

        if (recallAll.WasPressedThisFrame())
        {
            HandleRecall();
        }
        
        // if (rightThrow.WasPerformedThisFrame() && targetSystem.rightTargetLaunch != null)
        // {
        //     if (targetSystem.rightTargetLaunch.GetCurrentType() == InteractorType.Projectile)
        //     {
        //         reusableData.RightThrow = true;
        //         _stateMachine.ChangeState(_stateMachine.ThrowState);
        //     }
        // }
        //
        // if (leftThrow.WasPerformedThisFrame() && targetSystem.leftTargetLaunch != null)
        // {
        //     if (targetSystem.leftTargetLaunch.GetCurrentType() == InteractorType.Projectile)
        //     {
        //         reusableData.LeftThrow = true;
        //         _stateMachine.ChangeState(_stateMachine.ThrowState);
        //     }
        // }
    }
}
