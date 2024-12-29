using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerMovementStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }
    public override void Enter()
    {
        SlowDown();
    }
}
