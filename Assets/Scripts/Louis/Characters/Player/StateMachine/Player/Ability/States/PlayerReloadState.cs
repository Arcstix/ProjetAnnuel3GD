using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReloadState : PlayerAbilityState
{
    public PlayerReloadState(PlayerAbilityStateMachine playerAbilityStateMachine) : base(playerAbilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Reload");

        if (_stateMachine.IsRight)
        {
            reusableData.CanUseRightAbility = false;
            if(reusableData.RightProjectileRef != null)
            {
                _stateMachine.ChangeState(_stateMachine.StandbyState);
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.ReadyState);
            }
        }
        else
        {
            reusableData.CanUseLeftAbility = false;
            if (reusableData.LeftProjectileRef != null)
            {
                _stateMachine.ChangeState(_stateMachine.StandbyState);
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.ReadyState);
            }
        }
    }
}
