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
        Debug.Log("Shoot");

        if (_stateMachine.IsRight)
        {
            _stateMachine.AbilityManager.UseAbility(_stateMachine.IsRight);
            reusableData.CanUseRightAbility = false;
        }
        else
        {
            _stateMachine.AbilityManager.UseAbility(_stateMachine.IsRight);
            reusableData.CanUseLeftAbility = false;
        }
    }
}
