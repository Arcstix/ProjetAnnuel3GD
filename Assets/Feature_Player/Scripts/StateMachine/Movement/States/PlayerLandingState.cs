using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerAirState
{
    public PlayerLandingState(PlayerMovementStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.ChangeState(stateMachine.IdleState);     
    }
}
