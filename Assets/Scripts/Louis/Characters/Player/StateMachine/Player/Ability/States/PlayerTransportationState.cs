using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTransportationState : PlayerAbilityState
{
    public PlayerTransportationState(PlayerAbilityStateMachine playerAbilityStateMachine) : base(playerAbilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Transportation");

        CheckTransportationType();

        SetCameraToThirdPerson(); // Méthode utilisé seulement si on a un script CameraManager attaché

        if (_stateMachine.IsRight)
        {
            input.PlayerActions.AttractionRight.canceled += HandleReload;
            reusableData.CanUseRightAbility = false;
        }
        else
        {
            input.PlayerActions.AttractionLeft.canceled += HandleReload;
            reusableData.CanUseLeftAbility = false;
        }
    }

    public override void Exit()
    {
        base.Exit();
        reusableData.OnTransportation = false;
        reusableData.CanMove = true;
        reusableData.ShouldSlowDown = true;
        input.PlayerActions.AttractionLeft.canceled -= HandleReload;
    }

    public override void FixedTick()
    {
        base.FixedTick();

        if (_stateMachine.IsRight)
        {
            if(reusableData.RightProjectileRef != null)
            {
                if (reusableData.LeftProjectileRef != null)
                {
                    RightMoveToLeft();
                }
                else
                {
                    MoveToProjectile();
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
                if (reusableData.RightProjectileRef != null)
                {
                    LeftMoveToRight();
                }
                else
                {
                    MoveToProjectile();
                }
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.ReloadState);
            }
        }      
    }

    /// <summary>
    /// Selon si l'autre Outil a été lancé ou non le comportement est différent 
    /// et s'il n'a pas été lancé alors on sera en déplacement spécial.
    /// </summary>
    private void CheckTransportationType()
    {
        if (_stateMachine.IsRight)
        {
            if (reusableData.LeftProjectileRef == null)
            {
                reusableData.OnTransportation = true;
                reusableData.CanMove = false;
            }
        }
        else
        {
            if (reusableData.RightProjectileRef == null)
            {
                reusableData.OnTransportation = true;
                reusableData.CanMove = false;
            }
        }
    }
}
