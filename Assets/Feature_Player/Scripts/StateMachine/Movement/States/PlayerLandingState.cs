using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerAirState
{
    public event Action OnLanding; 
    
    public PlayerLandingState(PlayerMovementStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        OnLanding?.Invoke();
        stateMachine.ChangeState(stateMachine.IdleState);     
    }
}
