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
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/activation droit");
        base.Enter();
        stateMachine.ChangeState(stateMachine.IdleState);     
    }
}
