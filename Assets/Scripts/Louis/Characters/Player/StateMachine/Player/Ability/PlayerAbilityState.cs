using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityState : IState
{
    protected PlayerAbilityStateMachine _playerAbilityStateMachine;
    protected PlayerAbilityData _abilityData;

    public PlayerAbilityState(PlayerAbilityStateMachine playerAbilityStateMachine)
    {
        _playerAbilityStateMachine = playerAbilityStateMachine;
        _abilityData = _playerAbilityStateMachine.AbilityManager.PlayerSO.AbilityData;
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

    protected virtual void AddInputCallBack()
    {
        _playerAbilityStateMachine.AbilityManager.Input.PlayerActions.Ability.started += TryAbility;
    }

    protected virtual void AddCancelInputCallBack()
    {
        _playerAbilityStateMachine.AbilityManager.Input.PlayerActions.CancelAbility.started += CancelAbility;
    }


    protected virtual void RemoveInputCallBack()
    {
        _playerAbilityStateMachine.AbilityManager.Input.PlayerActions.Ability.started -= TryAbility;
    }

    protected virtual void RemoveCancelInputCallBack()
    {
        _playerAbilityStateMachine.AbilityManager.Input.PlayerActions.CancelAbility.started -= CancelAbility;
    }

    private void CancelAbility(InputAction.CallbackContext context)
    {
        _playerAbilityStateMachine.ChangeState(_playerAbilityStateMachine.CancelAbilityState);
    }

    private void TryAbility(InputAction.CallbackContext context)
    {
        if (_playerAbilityStateMachine.ReusableStateData.CanUseAbility)
        {
            if(_playerAbilityStateMachine.ReusableStateData.ProjectileRef == null)
            {
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