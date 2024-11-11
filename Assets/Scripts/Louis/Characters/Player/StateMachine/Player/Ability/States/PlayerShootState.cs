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
        RemoveInputShoot();
        reusableData.CanUseAbility = false;
        _playerAbilityStateMachine.AbilityManager.UseAbility();
    }
}
