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
            //movementStateMachine.MovementManager.Rigidbody.AddForce(movementStateMachine.MovementManager.Rigidbody.velocity.y * movementStateMachine.MovementManager.Rigidbody.GravityModifier); //trouver le chemin jusqu'à GroundedData qui a la modifier de gravité
        }
    }
}
