using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityState : IState
{
    // Mettre les variables "protected" pour que les States puissent y avoir acc�s.
    protected PlayerAbilityStateMachine _playerAbilityStateMachine;
    protected PlayerMetricsManager metricsManager;
    protected PlayerInput input;
    protected PlayerCameraManager cameraManager;
    protected PlayerReusableStateData reusableData;
    protected Rigidbody rigidbody;

    // Sert � la cr�ation de raccourcie.
    // ATTENTION AUX SCRIPTABLES OBJECTS QUI PEUVENT TOTALEMENT CHANGER COMME LE PLAYERSO !
    public PlayerAbilityState(PlayerAbilityStateMachine playerAbilityStateMachine)
    {
        _playerAbilityStateMachine = playerAbilityStateMachine;
        metricsManager = _playerAbilityStateMachine.AbilityManager.Metrics;
        input = _playerAbilityStateMachine.AbilityManager.Input;
        reusableData = _playerAbilityStateMachine.ReusableStateData;
        rigidbody = _playerAbilityStateMachine.AbilityManager.Rigidbody;

        if(_playerAbilityStateMachine.AbilityManager.CameraManager != null)
        {
            cameraManager = _playerAbilityStateMachine.AbilityManager.CameraManager;
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

    #region Ability methods

    /// <summary>
    /// On s'abonne � l'input qui sert � utiliser l'abilit�.
    /// </summary>
    protected virtual void AddInputAbility()
    {
        input.PlayerActions.ThrowRecallRight.started += TryAbility;
    }

    /// <summary>
    /// On se d�sabonne � l'input qui sert � shoot.
    /// </summary>
    protected virtual void RemoveInputShoot()
    {
        input.PlayerActions.ThrowRecallRight.started -= TryAbility;
    }

    /// <summary>
    /// On s'abonne � l'input qui sert � ramener l'objet.
    /// </summary>
    protected virtual void AddCancelInput()
    {
        input.PlayerActions.CancelAbility.started += CancelAbility;
    }

    /// <summary>
    /// On se d�sabonne � l'input qui sert � ramener l'objet.
    /// </summary>
    protected virtual void RemoveCancelInput()
    {
        input.PlayerActions.CancelAbility.started -= CancelAbility;
    }

    /// <summary>
    /// Si le joueur appuie sur l'input CancelAbility alors qu'on est abonn� on change d'�tat
    /// </summary>
    /// <param name="context"></param>
    private void CancelAbility(InputAction.CallbackContext context)
    {
        _playerAbilityStateMachine.ChangeState(_playerAbilityStateMachine.CancelAbilityState);
    }

    /// <summary>
    /// M�thode appel� quand le joueur est abonn� � l'input Ability et appuie sur l'input
    /// </summary>
    /// <param name="context"></param>
    private void TryAbility(InputAction.CallbackContext context)
    {
        if (reusableData.CanUseAbility)
        {
            if(reusableData.ProjectileRef == null)
            {
                if (metricsManager.CurrentPlayerSO.AbilityData.ShootData.UseAim)
                {
                    if (cameraManager != null && !cameraManager.IsFirstPerson) { return; }
                }

                _playerAbilityStateMachine.ChangeState(_playerAbilityStateMachine.ShootState);
                return;
            }
            else
            {
                _playerAbilityStateMachine.ChangeState(_playerAbilityStateMachine.TransportationState);
                return;
            }
        }
    }

    #endregion


    #region Reusable methods

    #endregion
}
