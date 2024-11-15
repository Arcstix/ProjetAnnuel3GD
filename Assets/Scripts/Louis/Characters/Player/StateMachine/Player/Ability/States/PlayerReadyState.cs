using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReadyState : PlayerAbilityState
{
    public PlayerReadyState(PlayerAbilityStateMachine playerAbilityStateMachine) : base(playerAbilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Ready");
        if (_stateMachine.IsRight)
        {
            AddInputRightThrowRecall();
            reusableData.CanUseRightAbility = true;
        }
        else
        {
            AddInputLeftThrowRecall();
            reusableData.CanUseLeftAbility = true;
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (_stateMachine.IsRight)
        {
            RemoveInputRightThrowRecall();
            reusableData.CanUseRightAbility = false;
        }
        else
        {
            RemoveInputLeftThrowRecall();
            reusableData.CanUseLeftAbility = false;
        }
    }
}
