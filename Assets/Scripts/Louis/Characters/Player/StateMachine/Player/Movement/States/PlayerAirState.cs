using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerMovementState
{
    public PlayerAirState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        
    }
    public void SlowDown()
    {
        if (movementStateMachine.ReusableData.InAir && !movementStateMachine.ReusableData.OnTransportation)
        {
           //movementStateMachine.ReusableData.Stat
        }
    }
}
