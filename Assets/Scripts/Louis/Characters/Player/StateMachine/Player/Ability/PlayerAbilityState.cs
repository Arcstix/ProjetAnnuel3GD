using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : IState
{
    private PlayerAbilityStateMachine PlayerAbilityStateMachine;

    public PlayerAbilityState(PlayerAbilityStateMachine playerAbilityStateMachine)
    {
        PlayerAbilityStateMachine = playerAbilityStateMachine;
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
        
    }
    #endregion


    #region Reusable methods

    #endregion
}
