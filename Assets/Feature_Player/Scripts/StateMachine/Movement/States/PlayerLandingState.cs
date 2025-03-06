using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlayerLandingState : PlayerAirState
{
    private float timer;
    
    public event Action OnLanding; 
    
    public PlayerLandingState(PlayerMovementStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        OnLanding?.Invoke();
    }

    public override void Tick()
    {
        base.Tick();
        
        timer += Time.deltaTime;

        if (timer >= 0.2f)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}
