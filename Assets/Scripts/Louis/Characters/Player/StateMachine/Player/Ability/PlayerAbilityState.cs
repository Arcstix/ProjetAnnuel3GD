using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityState : IState
{
    protected PlayerAbilityStateMachine playerAbilityStateMachine;

    public PlayerAbilityState(PlayerAbilityStateMachine playerAbilityStateMachine)
    {
        this.playerAbilityStateMachine = playerAbilityStateMachine;
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
        ReadAbilityInput();
    }
    #endregion

    #region Ability methods
    private void ReadAbilityInput()
    {
        playerAbilityStateMachine.StateMachine.Input.PlayerActions.Ability.started += TryAbility;
    }

    private void TryAbility(InputAction.CallbackContext context)
    {
        
    }
    #endregion


    #region Reusable methods

    #endregion
}
