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
        //_playerAbilityStateMachine.AbilityManager.Rigidbody.useGravity = false;
        _playerAbilityStateMachine.ReusableStateData.OnTransportation = true;

        if(_playerAbilityStateMachine.AbilityManager.CameraManager != null)
        {
            _playerAbilityStateMachine.AbilityManager.CameraManager.IsFirstPerson = false;
            _playerAbilityStateMachine.AbilityManager.CameraManager.ThirdPersonMode();
        }
        
        _playerAbilityStateMachine.ReusableStateData.CanMove = false;
        
        RemoveInputCallBack();
        _playerAbilityStateMachine.ReusableStateData.CanUseAbility = false;
        _playerAbilityStateMachine.AbilityManager.Input.PlayerActions.Jump.started += ChangeToPlayerJumpState;
        
    }

    private void ChangeToPlayerJumpState(InputAction.CallbackContext context)
    {
        _playerAbilityStateMachine.ChangeState(_playerAbilityStateMachine.ReloadAbilityState); // On sort de l'état transportation
        // On ordonne de passer dans l'état jump dans la statemachine du movement

    }

    public override void Exit()
    {
        base.Exit();
        _playerAbilityStateMachine.ReusableStateData.OnTransportation = false;
        //_playerAbilityStateMachine.AbilityManager.Rigidbody.useGravity = true;
        _playerAbilityStateMachine.ReusableStateData.CanMove = true;
        _playerAbilityStateMachine.ReusableStateData.ShouldSlowDown = true;
    }

    public override void FixedTick()
    {
        base.FixedTick();

        if(_playerAbilityStateMachine.ReusableStateData.ProjectileRef != null)
        {
            MoveToProjectile();
        }
    }

    private void MoveToProjectile()
    {
        Vector3 direction = (_playerAbilityStateMachine.ReusableStateData.ProjectileRef.transform.position - _playerAbilityStateMachine.AbilityManager.Rigidbody.worldCenterOfMass).normalized;

        //float distanceRemain = Vector3.Distance(_playerAbilityStateMachine.AbilityManager.transform.position, _playerAbilityStateMachine.ReusableStateData.ProjectileRef.transform.position);


        //float speedModifier = _abilityData.TransportationData.SpeedModifier.Evaluate(distanceRemain);

        _playerAbilityStateMachine.AbilityManager.Rigidbody.AddForce(direction * metricsManager.CurrentPlayerSO.AbilityData.TransportationData.BaseSpeed - _playerAbilityStateMachine.AbilityManager.Rigidbody.velocity, ForceMode.VelocityChange);
    }

}
