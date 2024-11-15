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
            input.PlayerActions.AttractionRight.started += HandleAttraction;
            input.PlayerActions.ThrowRecallRight.started += HandleRecall;
            reusableData.CanUseRightAbility = true;
        }
        else
        {
            input.PlayerActions.AttractionLeft.started += HandleAttraction;
            input.PlayerActions.ThrowRecallLeft.started += HandleRecall;
            reusableData.CanUseLeftAbility = true;
        }   
    }

    public override void Exit()
    {
        base.Exit();
        if (_stateMachine.IsRight)
        {
            input.PlayerActions.AttractionRight.started -= HandleAttraction;
            input.PlayerActions.ThrowRecallRight.started -= HandleRecall;
            reusableData.CanUseRightAbility = false;
        }
        else
        {
            input.PlayerActions.AttractionLeft.started -= HandleAttraction;
            input.PlayerActions.ThrowRecallLeft.started -= HandleRecall;
            reusableData.CanUseLeftAbility = false;
        }
    }

    protected void HandleAttraction(InputAction.CallbackContext context)
    {
        if (reusableData.TransportationMode)
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
