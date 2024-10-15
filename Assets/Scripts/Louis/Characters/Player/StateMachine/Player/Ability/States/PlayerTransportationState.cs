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

        _playerAbilityStateMachine.ReusableStateData.CanMove = false;
        Debug.Log("Transportation State");
        Debug.Log(_playerAbilityStateMachine.ReusableStateData.CanMove);
        RemoveInputCallBack();
        _playerAbilityStateMachine.ReusableStateData.CanUseAbility = false;
    }

    public override void Exit()
    {
        base.Exit();

        _playerAbilityStateMachine.ReusableStateData.CanMove = true;
        Debug.Log(_playerAbilityStateMachine.ReusableStateData.CanMove);
    }

    public override void FixedTick()
    {
        base.FixedTick();

        float distance = Vector3.Distance(_playerAbilityStateMachine.AbilityManager.LauncherTransform.position, _playerAbilityStateMachine.ReusableStateData.ProjectileRef.transform.position);

        if(distance > 0.5f)
        {
            MoveToProjectile();
            return;
        }

        _playerAbilityStateMachine.ChangeState(_playerAbilityStateMachine.ReloadAbilityState);
    }

    private void MoveToProjectile()
    {
        Vector3 direction = (_playerAbilityStateMachine.ReusableStateData.ProjectileRef.transform.position - _playerAbilityStateMachine.AbilityManager.LauncherTransform.position).normalized;
        //direction.y = 0;

        _playerAbilityStateMachine.AbilityManager.Rigidbody.AddForce(direction * _abilityData.TransportationData.BaseSpeed - _playerAbilityStateMachine.AbilityManager.Rigidbody.velocity, ForceMode.VelocityChange);
    }
}
