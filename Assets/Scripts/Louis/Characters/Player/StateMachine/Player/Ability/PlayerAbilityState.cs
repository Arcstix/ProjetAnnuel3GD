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
        _abilityData = _playerAbilityStateMachine.StateMachine.PlayerSO.AbilityData;
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
        _playerAbilityStateMachine.StateMachine.Input.PlayerActions.Ability.started += TryAbility;
    }

    protected virtual void RemoveInputCallBack()
    {
        _playerAbilityStateMachine.StateMachine.Input.PlayerActions.Ability.started -= TryAbility;
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
