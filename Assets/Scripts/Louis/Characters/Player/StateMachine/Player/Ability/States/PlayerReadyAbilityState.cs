using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReadyAbilityState : PlayerAbilityState
{
    public PlayerReadyAbilityState(PlayerAbilityStateMachine playerAbilityStateMachine) : base(playerAbilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Ready State");
        AddInputCallBack();
        _playerAbilityStateMachine.ReusableStateData.CanUseAbility = true;
        _playerAbilityStateMachine.ReusableStateData.ProjectileRef = null;
    }

    public override void Exit()
    {
        base.Exit();

        RemoveInputCallBack();
    }
}
