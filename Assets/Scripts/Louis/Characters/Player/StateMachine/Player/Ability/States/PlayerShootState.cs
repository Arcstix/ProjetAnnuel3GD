using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootState : PlayerAbilityState
{
    public PlayerShootState(PlayerAbilityStateMachine playerAbilityStateMachine) : base(playerAbilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Shoot State");
        RemoveInputCallBack();
        _playerAbilityStateMachine.ReusableStateData.CanUseAbility = false;
        _playerAbilityStateMachine.StateMachine.UseAbility();
    }
}
