using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityTransportState : AbilityState
{
    protected InteractionSystem leftInteraction;
    protected InteractionSystem rightInteraction;
    protected InteractionSystem playerInteraction;
    
    public AbilityTransportState(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
    }
    
    public override void Tick()
    {
        base.Tick();

        if (rightAttraction.IsPressed() && leftAttraction.IsPressed())
        {
            if (reusableData.RightObject != null && reusableData.LeftObject != null)
            {
                _stateMachine.ChangeState(_stateMachine.MoveBothObject);
            }
        }
        
        ConditionExitTransport();
    }

    protected void ConditionExitTransport()
    {
        // Important car on ne peut pas rester dans une Transport State si ce n'est pas vérifié.
        if (reusableData.RightObject == null && reusableData.LeftObject == null)
        {
            reusableData.LeftParent = null;
            reusableData.RightParent = null;
            
            leftLauncher.GetComponent<MeshRenderer>().enabled = true;
            rightLauncher.GetComponent<MeshRenderer>().enabled = true;
            
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }
        
        if (leftAttraction.WasReleasedThisFrame() && !rightAttraction.IsPressed())
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }

        if (rightAttraction.WasReleasedThisFrame() && !leftAttraction.IsPressed())
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
    
    protected void CheckStamina()
    {
        if (reusableData.RightActivation)
        {
            if (metricsManager.StaminaRight <= 0)
            {
                reusableData.RightInput = true;
                _stateMachine.ChangeState(_stateMachine.RecallState);
                return;
            }
        }

        if (reusableData.LeftActivation)
        {
            if (metricsManager.StaminaLeft <= 0)
            {
                reusableData.LeftInput = true;
                _stateMachine.ChangeState(_stateMachine.RecallState);
                return;
            }
        }
    }

    protected void MoveLeftObjectToLaunch()
    {
        leftInteraction.Interactable(false);
        if (targetSystem.leftTargetLaunch.GetCurrentType() == InteractorType.Projectile)
        {
            reusableData.LeftObject.SetLaunch(true);
        }
    }
    
    protected void MoveRightObjectToLaunch()
    {
        rightInteraction.Interactable(false);
        if (targetSystem.rightTargetLaunch.GetCurrentType() == InteractorType.Projectile)
        {
            reusableData.RightObject.SetLaunch(true);
        }
    }

    public void ResetBoolActivationTransport()
    {
        reusableData.LeftActivation = false;
        reusableData.RightActivation = false;
        reusableData.OnTransportation = false;
    }
}
