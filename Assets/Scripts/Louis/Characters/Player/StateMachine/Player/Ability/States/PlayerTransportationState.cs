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

        Debug.Log("Transportation State");
        RemoveInputCallBack();
        _playerAbilityStateMachine.ReusableStateData.CanUseAbility = false;
    }

    public override void FixedTick()
    {
        base.FixedTick();

        float distance = Vector3.Distance(_playerAbilityStateMachine.StateMachine.LauncherTransform.position, _playerAbilityStateMachine.ReusableStateData.ProjectileRef.transform.position);

        if(distance > 0.5f)
        {
            MoveToProjectile();
            return;
        }

        _playerAbilityStateMachine.ChangeState(_playerAbilityStateMachine.ReloadAbilityState);
    }

    private void MoveToProjectile()
    {
        Vector3 direction = (_playerAbilityStateMachine.ReusableStateData.ProjectileRef.transform.position - _playerAbilityStateMachine.StateMachine.LauncherTransform.position).normalized;
        //direction.y = 0;

        _playerAbilityStateMachine.StateMachine.Rigidbody.AddForce(direction * _abilityData.TransportationData.BaseSpeed - _playerAbilityStateMachine.StateMachine.Rigidbody.velocity, ForceMode.VelocityChange);
    }
}
