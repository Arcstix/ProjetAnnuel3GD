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
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/TIr droit");
            _stateMachine.AbilityManager.UseAbility(_stateMachine.IsRight);
            reusableData.CanUseRightAbility = false;
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Tir gauche");
            _stateMachine.AbilityManager.UseAbility(_stateMachine.IsRight);
            reusableData.CanUseLeftAbility = false;
        }
    }
}
