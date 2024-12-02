using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStandbyState : PlayerAbilityState
{
    public PlayerStandbyState(PlayerAbilityStateMachine playerAbilityStateMachine) : base(playerAbilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Stanby");


        if (_stateMachine.IsRight)
        {
            if(reusableData.LeftProjectileRef == null)
            {
                input.PlayerActions.AttractionRight.started += ActivateRight;
                input.PlayerActions.AttractionLeft.started += ActivateLeft;
            }            
            input.PlayerActions.ThrowRecallRight.started += HandleRecall;
            reusableData.CanUseRightAbility = true;
        }
        else
        {
            if(reusableData.RightProjectileRef == null)
            {
                input.PlayerActions.AttractionLeft.started += ActivateLeft;
                input.PlayerActions.AttractionRight.started += ActivateRight;
            }            
            input.PlayerActions.ThrowRecallLeft.started += HandleRecall;
            reusableData.CanUseLeftAbility = true;
        }   
    }

    public override void Exit()
    {
        base.Exit();
        if (_stateMachine.IsRight)
        {
            input.PlayerActions.AttractionRight.started -= ActivateRight;
            input.PlayerActions.ThrowRecallRight.started -= HandleRecall;
            reusableData.CanUseRightAbility = false;
        }
        else
        {
            input.PlayerActions.AttractionLeft.started -= ActivateLeft;
            input.PlayerActions.ThrowRecallLeft.started -= HandleRecall;
            reusableData.CanUseLeftAbility = false;
        }
    }

    protected void ActivateRight(InputAction.CallbackContext context)
    {
        if(reusableData.LeftProjectileRef == null)
        {
            _stateMachine.ChangeState(_stateMachine.TransportationState);
        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.AttractionState);
        }
    }

    private void ActivateLeft(InputAction.CallbackContext context)
    {
        if (reusableData.RightProjectileRef == null)
        {
            _stateMachine.ChangeState(_stateMachine.TransportationState);
        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.AttractionState);
        }
    }

    private void HandleRecall(InputAction.CallbackContext context)
    {
        _stateMachine.ChangeState(_stateMachine.RecallState);
    }
}
