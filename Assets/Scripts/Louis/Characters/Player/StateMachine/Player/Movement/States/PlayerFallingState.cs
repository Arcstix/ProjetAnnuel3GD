using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerAirState
{
    public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Falling");

    }
    public override void Tick()
    {
        base.Tick();
        
        if (movementStateMachine.ReusableData.ShouldSlowDown)
        {
            SlowDown();
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                movementStateMachine.ReusableData.ShouldSlowDown = false;
                timer = 0;
            }
        }
        if(!movementStateMachine.ReusableData.InAir)
        {
            movementStateMachine.ChangeState(movementStateMachine.LandingState);
        }
    }


    public override void Exit()
    {
        base.Exit();
    }

    
}

