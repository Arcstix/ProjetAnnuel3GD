using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerAirState
{
    public PlayerLandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
     
    }
    public override void Tick()
    {
        base.Tick();
    }
    public override void Exit()
    {
        base.Exit();
    }

}
