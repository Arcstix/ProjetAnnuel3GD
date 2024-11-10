using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerMovementState
{
    //créer des raccourcies pour les variables
    protected PlayerGroundedData groundedData;
    protected float timer;
    override public void Tick()
    {
        base.Tick();
        Debug.Log("AirState");
        if (movementStateMachine.ReusableData.InAir && movementStateMachine.currentState != movementStateMachine.FallingState && !movementStateMachine.ReusableData.OnTransportation)
        {
            movementStateMachine.ChangeState(movementStateMachine.FallingState);
        }
    }
    public PlayerAirState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        //faire le chemin ici comme ça = une fois
        groundedData = movementStateMachine.MovementManager.Metrics.CurrentPlayerSO.GroundedData;
    }
    public void SlowDown()
    {
     
         //ajouter une force pour ralentir le joueur avec une animation curve
         movementStateMachine.MovementManager.Rigidbody.AddForce(new Vector3(0,movementStateMachine.MovementManager.Rigidbody.velocity.y * groundedData.GravityModifier.Evaluate(timer),0)); //trouver le chemin jusqu'à GroundedData qui a la modifier de gravité
         Debug.Log("SlowDown");
     
    }
}
