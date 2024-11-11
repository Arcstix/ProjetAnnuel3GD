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
        Debug.Log("Rappel Objet");
        RemoveInputShoot();
        reusableData.CanUseAbility = false;
    }

    public override void FixedTick()
    {
        base.FixedTick();
        reusableData.ProjectileRef.ReturnToPlayer();
    }
}
