using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Script utilisé quand le joueur est dans une state lié au sol
/// </summary>
public class PlayerGroundedState : PlayerMovementState
{
    private CapsuleColliderUtility capsuleColliderUtility;
    private PlayerGroundedData groundedData;

    public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        capsuleColliderUtility = movementStateMachine.MovementManager.CapsuleColliderUtility;
        groundedData = movementStateMachine.MovementManager.Metrics.CurrentPlayerSO.GroundedData;
    }

    public override void FixedTick()
    {
        base.FixedTick();

        Debug.Log("Etat Transportation " + movementStateMachine.ReusableData.OnTransportation);

        if (!movementStateMachine.ReusableData.OnTransportation)
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

            float distanceToGround = capsuleColliderUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y * movementStateMachine.MovementManager.transform.localScale.y - hit.distance;

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

    protected override void SubscribeInputAction()
    {
        base.SubscribeInputAction();

        input.PlayerActions.Movement.canceled += OnMovementCanceled;
    }

    protected override void UnsubscribeInputAction()
    {
        base.UnsubscribeInputAction();

        input.PlayerActions.Movement.canceled -= OnMovementCanceled;
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (!reusableData.InAir && movementStateMachine.currentState != movementStateMachine.IdleState)
        {
            movementStateMachine.ChangeState(movementStateMachine.IdleState);
        }
    }

    /// <summary>
    /// Method use when the player change to a moving state
    /// </summary>
    protected virtual void OnMove()
    {
        if (reusableData.ShouldWalk)
        {
            movementStateMachine.ChangeState(movementStateMachine.SlowState);
            return;
        }

        movementStateMachine.ChangeState(movementStateMachine.RunningState);
    }
}
