using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransportationState : PlayerAbilityState
{
    public PlayerTransportationState(PlayerAbilityStateMachine playerAbilityStateMachine) : base(playerAbilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _playerAbilityStateMachine.AbilityManager.Rigidbody.useGravity = true;
        _playerAbilityStateMachine.ReusableStateData.OnTransportation = true;
        _playerAbilityStateMachine.AbilityManager.CameraManager.IsFirstPerson = false;
        _playerAbilityStateMachine.AbilityManager.CameraManager.ThirdPersonMode();
        _playerAbilityStateMachine.ReusableStateData.CanMove = false;
        RemoveInputCallBack();
        _playerAbilityStateMachine.ReusableStateData.CanUseAbility = false;
    }

    public override void Exit()
    {
        base.Exit();
        _playerAbilityStateMachine.ReusableStateData.OnTransportation = false;
        _playerAbilityStateMachine.AbilityManager.Rigidbody.useGravity = true;
        _playerAbilityStateMachine.ReusableStateData.CanMove = true;
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
