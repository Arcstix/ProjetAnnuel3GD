using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReloadAbilityState : PlayerAbilityState
{
    public PlayerReloadAbilityState(PlayerAbilityStateMachine playerAbilityStateMachine) : base(playerAbilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Reload");
        RemoveInputShoot();
        reusableData.CanUseAbility = false;
        //_playerAbilityStateMachine.ReusableStateData.ProjectileRef = null;
        _playerAbilityStateMachine.ChangeState(_playerAbilityStateMachine.ReadyAbilityState);
    }
}
