using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecallState : PlayerAbilityState
{
    public PlayerRecallState(PlayerAbilityStateMachine playerAbilityStateMachine) : base(playerAbilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Rappel Objet");

        if (_stateMachine.IsRight)
        {
            reusableData.CanUseRightAbility = false;
        }
        else
        {
            reusableData.CanUseLeftAbility = false;
        }
    }

    public override void FixedTick()
    {
        base.FixedTick();

        if (_stateMachine.IsRight)
        {
            if(reusableData.RightProjectileRef != null)
            {
                reusableData.RightProjectileRef.ReturnToPlayer();
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.ReloadState);
            }
        }
        else
        {
            if (reusableData.LeftProjectileRef != null)
            {
                reusableData.LeftProjectileRef.ReturnToPlayer();
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.ReloadState);
            }
        }
    }
}
