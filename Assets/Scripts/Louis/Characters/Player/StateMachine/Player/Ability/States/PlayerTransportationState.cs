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
        //_playerAbilityStateMachine.AbilityManager.Rigidbody.useGravity = false;
        reusableData.OnTransportation = true;

        SetCameraToThirdPerson(); // Méthode utilisé seulement si on a un script CameraManager attaché

        reusableData.CanMove = false;
        RemoveInputShoot();
        reusableData.CanUseAbility = false;
        input.PlayerActions.Jump.started += ChangeToPlayerJumpState;

    }

    private void SetCameraToThirdPerson()
    {
        if (cameraManager != null)
        {
            cameraManager.IsFirstPerson = false;
            cameraManager.ThirdPersonMode();
        }
    }

    private void ChangeToPlayerJumpState(InputAction.CallbackContext context)
    {
        _playerAbilityStateMachine.ChangeState(_playerAbilityStateMachine.ReloadAbilityState); // On sort de l'état transportation
        // On ordonne de passer dans l'état jump dans la statemachine du movement
    }

    public override void Exit()
    {
        base.Exit();
        reusableData.OnTransportation = false;
        //_playerAbilityStateMachine.AbilityManager.Rigidbody.useGravity = true;
        reusableData.CanMove = true;
        reusableData.ShouldSlowDown = true;
    }

    public override void FixedTick()
    {
        base.FixedTick();

        if(reusableData.ProjectileRef != null)
        {
            MoveToProjectile();
        }
    }

    private void MoveToProjectile()
    {
        Vector3 direction = (reusableData.ProjectileRef.transform.position - rigidbody.worldCenterOfMass).normalized;

        //float distanceRemain = Vector3.Distance(_playerAbilityStateMachine.AbilityManager.transform.position, _playerAbilityStateMachine.ReusableStateData.ProjectileRef.transform.position);


        //float speedModifier = _abilityData.TransportationData.SpeedModifier.Evaluate(distanceRemain);

        rigidbody.AddForce(direction * metricsManager.CurrentPlayerSO.AbilityData.TransportationData.BaseSpeed - rigidbody.velocity, ForceMode.VelocityChange);
    }

}
