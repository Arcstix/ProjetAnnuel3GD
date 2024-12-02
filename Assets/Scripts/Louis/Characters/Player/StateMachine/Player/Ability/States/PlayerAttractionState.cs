using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttractionState : PlayerAbilityState
{
    public PlayerAttractionState(PlayerAbilityStateMachine playerAbilityStateMachine) : base(playerAbilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Attraction");

        if (_stateMachine.IsRight)
        {
            input.PlayerActions.AttractionRight.canceled += HandleReload;
            input.PlayerActions.AttractionLeft.canceled += HandleReload;
            reusableData.OnRightAttraction = true;
            reusableData.CanUseRightAbility = false;
        }
        else
        {
            input.PlayerActions.AttractionLeft.canceled += HandleReload;
            input.PlayerActions.AttractionRight.canceled += HandleReload;
            reusableData.OnLeftAttraction = true;
            reusableData.CanUseLeftAbility = false;
        }
    }

    public override void FixedTick()
    {
        base.FixedTick();

        Attraction();
    }

    private void Attraction()
    {
        if (_stateMachine.IsRight)
        {
            if(reusableData.RightProjectileRef != null)
            {
                if (reusableData.LeftProjectileRef == null)
                {
                    reusableData.RightProjectileRef.MoveToPlayer();
                }
                else
                {
                    RightMoveToLeft();
                }
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.ReloadState);
            }
        }
        else
        {
            if(reusableData.LeftProjectileRef != null)
            {
                if (reusableData.RightProjectileRef == null)
                {
                    reusableData.LeftProjectileRef.MoveToPlayer();
                }
                else
                {
                    LeftMoveToRight();
                }
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.ReloadState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (_stateMachine.IsRight)
        {
            input.PlayerActions.AttractionRight.canceled -= HandleReload;
            reusableData.OnRightAttraction = false;
        }
        else
        {
            input.PlayerActions.AttractionRight.canceled -= HandleReload;
            reusableData.OnLeftAttraction = false;
        }        
    }
}
