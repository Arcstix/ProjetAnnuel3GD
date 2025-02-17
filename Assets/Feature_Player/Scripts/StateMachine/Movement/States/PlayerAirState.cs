using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script utilis� quand le joueur est dans les airs
/// </summary>
public class PlayerAirState : PlayerMovementState
{
    //cr�er des raccourcies pour les variables
    protected CapsuleColliderUtility capsuleColliderUtility;
    protected GroundedData groundedData;
    protected float timer;

    public PlayerAirState(PlayerMovementStateMachine playerStateMachine) : base(playerStateMachine)
    {
        //faire le chemin ici comme �a = une fois
        capsuleColliderUtility = stateMachine.MovementManager.CapsuleColliderUtility;
        groundedData = stateMachine.MovementManager.Metrics.CurrentMetrics.GroundedData;
    }

    public override void FixedTick()
    {
        base.FixedTick();

        if (!reusableData.OnTransportation)
        {
            CheckDistanceToTheGround();
        }
    }

    protected void CheckDistanceToTheGround()
    {
        Vector3 capsuleColliderCenterInWorldSpace = capsuleColliderUtility.CapsuleColliderData.Collider.bounds.center;

        Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

        if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, capsuleColliderUtility.SlopeData.DistanceGroundCheck, capsuleColliderUtility.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
        {
            reusableData.CanMove = true;
            reusableData.InAir = false;
        }
        else
        {
            reusableData.InAir = true;
        }
    }

    public void SlowDown()
    {
        //ajouter une force pour ralentir le joueur avec une animation curve
        rigidbody.AddForce(Physics.gravity * (groundedData.GravityMultiplier * groundedData.GravityModifier.Evaluate(timer)) - GetCurrentVerticalVelocity(), ForceMode.Acceleration);
    }
}
