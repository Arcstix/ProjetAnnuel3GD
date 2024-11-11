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
        Debug.Log("Ready");
        AddInputAbility();
        reusableData.CanUseAbility = true;
    }

    public override void Exit()
    {
        base.Exit();

        RemoveInputShoot();
    }
}
