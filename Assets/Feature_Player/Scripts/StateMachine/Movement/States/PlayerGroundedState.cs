using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Script utilis� quand le joueur est dans une state li� au sol
/// </summary>
public class PlayerGroundedState : PlayerMovementState
{
    private CapsuleColliderUtility capsuleColliderUtility;
    private GroundedData groundedData;

    public PlayerGroundedState(PlayerMovementStateMachine playerStateMachine) : base(playerStateMachine)
    {
        capsuleColliderUtility = stateMachine.MovementManager.CapsuleColliderUtility;
        groundedData = stateMachine.MovementManager.Metrics.CurrentPlayerSO.GroundedData;
    }

    public override void FixedTick()
    {
        base.FixedTick();

        if (!stateMachine.ReusableData.OnTransportation)
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
            float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

            float distanceToGround = capsuleColliderUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y * stateMachine.MovementManager.transform.localScale.y - hit.distance;

            if (distanceToGround == 0f)
            {
                return;
            }

            float floatSpeedModifier = SetSlopeSpeedModifierOnAngle(groundAngle);

            if (floatSpeedModifier == 0f)
            {
                return;
            }

            AddVerticalForce(distanceToGround, capsuleColliderUtility.SlopeData.StepReachForce);
        }
        else
        {
            reusableData.InAir = true;
        }
    }

    protected float SetSlopeSpeedModifierOnAngle(float angle)
    {
        float slopeSpeedModifier = groundedData.SlopeSpeedAngle.Evaluate(angle);

        reusableData.MovementOnSlopeSpeedModifier = slopeSpeedModifier;

        return slopeSpeedModifier;
    }

    protected void AddVerticalForce(float distanceToGround, float forceMultiplier = 10f)
    {
        float amountToLift = distanceToGround * forceMultiplier - GetCurrentVerticalVelocity().y;

        Vector3 liftForce = new Vector3(0f, amountToLift, 0f);

        rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Method use when the player change to a moving state
    /// </summary>
    protected virtual void OnMove()
    {
        if (reusableData.ShouldWalk)
        {
            stateMachine.ChangeState(stateMachine.SlowState);
            return;
        }

        stateMachine.ChangeState(stateMachine.RunningState);
    }
}
