using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityState : IState
{
    // Mettre les variables "protected" pour que les States puissent y avoir accès.
    protected PlayerAbilityStateMachine _stateMachine;
    protected PlayerMetricsManager metricsManager;
    protected PlayerInput input;
    protected PlayerCameraManager cameraManager;
    protected PlayerReusableStateData reusableData;
    protected Rigidbody rigidbody;

    // Sert à la création de raccourcie.
    // ATTENTION AUX SCRIPTABLES OBJECTS QUI PEUVENT TOTALEMENT CHANGER COMME LE PLAYERSO !
    public PlayerAbilityState(PlayerAbilityStateMachine playerAbilityStateMachine)
    {
        _stateMachine = playerAbilityStateMachine;
        metricsManager = _stateMachine.AbilityManager.Metrics;
        input = _stateMachine.AbilityManager.Input;
        reusableData = _stateMachine.ReusableStateData;
        rigidbody = _stateMachine.AbilityManager.Rigidbody;

        if(_stateMachine.AbilityManager.CameraManager != null)
        {
            cameraManager = _stateMachine.AbilityManager.CameraManager;
        }
    }

    #region State Methods
    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public virtual void Tick()
    {
        
    }

    public virtual void FixedTick()
    {
        
    }

    public virtual void HandleInput()
    {
        
    }
    #endregion

    #region Input method

    /// <summary>
    /// On s'abonne à l'input qui sert à Shoot et Recall l'objet de droite
    /// </summary>
    protected virtual void AddInputRightThrowRecall()
    {
        input.PlayerActions.ThrowRecallRight.started += HandleRightThrowRecall;
    }

    /// <summary>
    /// On s'abonne à l'input qui sert à Shoot et Recall l'objet de gauche
    /// </summary>
    protected virtual void AddInputLeftThrowRecall()
    {
        input.PlayerActions.ThrowRecallLeft.started += HandleLeftThrowRecall;
    }

    /// <summary>
    /// On se désabonne à l'input qui sert à shoot et recall l'objet de droite
    /// </summary>
    protected virtual void RemoveInputRightThrowRecall()
    {
        input.PlayerActions.ThrowRecallRight.started -= HandleRightThrowRecall;
    }

    /// <summary>
    /// On se désabonne à l'input qui sert à shoot et recall l'objet de gauche
    /// </summary>
    protected virtual void RemoveInputLeftThrowRecall()
    {
        input.PlayerActions.ThrowRecallLeft.started -= HandleLeftThrowRecall;
    }

    #endregion

    #region Ability methods

    /// <summary>
    /// Méthode appelé quand le joueur est abonné à l'input Ability et appuie sur l'input
    /// </summary>
    /// <param name="context"></param>
    protected void HandleRightThrowRecall(InputAction.CallbackContext context)
    {
        if (reusableData.CanUseRightAbility)
        {
            if(reusableData.RightProjectileRef == null)
            {
                _stateMachine.ChangeState(_stateMachine.ShootState);
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.RecallState);
            }
        }
    }


    protected void HandleLeftThrowRecall(InputAction.CallbackContext context)
    {
        if (reusableData.CanUseLeftAbility)
        {
            if (reusableData.LeftProjectileRef == null)
            {
                _stateMachine.ChangeState(_stateMachine.ShootState);
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.RecallState);
            }
        }
    }

    protected void HandleReload(InputAction.CallbackContext context)
    {
        _stateMachine.ChangeState(_stateMachine.ReloadState);
    }

    #endregion


    #region Reusable methods

    protected void SetCameraToThirdPerson()
    {
        if (cameraManager != null)
        {
            cameraManager.IsFirstPerson = false;
            cameraManager.ThirdPersonMode();
        }
    }

    protected void MoveToProjectile()
    {
        Vector3 direction = new Vector3();

        if (_stateMachine.IsRight)
        {
            direction = (reusableData.RightProjectileRef.transform.position - rigidbody.worldCenterOfMass).normalized;
        }
        else
        {
            direction = (reusableData.LeftProjectileRef.transform.position - rigidbody.worldCenterOfMass).normalized;
        }

        rigidbody.AddForce(direction * metricsManager.CurrentPlayerSO.AbilityData.TransportationData.BaseSpeed - rigidbody.velocity, ForceMode.VelocityChange);
    }

    protected void LeftMoveToRight()
    {
        Vector3 direction = (reusableData.RightProjectileRef.transform.position - reusableData.LeftProjectileRef.transform.position).normalized;

        reusableData.LeftProjectileRef.MoveToDirection(direction, reusableData.RightProjectileRef.transform.position, reusableData.LeftProjectileRef.transform.position);
    }

    protected void RightMoveToLeft()
    {
        Vector3 direction = (reusableData.LeftProjectileRef.transform.position - reusableData.RightProjectileRef.transform.position).normalized;

        reusableData.RightProjectileRef.MoveToDirection(direction, reusableData.LeftProjectileRef.transform.position, reusableData.RightProjectileRef.transform.position);
    }
    #endregion
}
