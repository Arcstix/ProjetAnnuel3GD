using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCancelAbilityState : PlayerAbilityState
{
    public PlayerCancelAbilityState(PlayerAbilityStateMachine playerAbilityStateMachine) : base(playerAbilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        RemoveInputCallBack();
        _playerAbilityStateMachine.ReusableStateData.CanUseAbility = false;
        _playerAbilityStateMachine.ReusableStateData.ProjectileRef.ReturnToPlayer();
    }
}
