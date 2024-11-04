using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerMovementState
{
    private CapsuleColliderUtility capsuleColliderUtility;

    public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        capsuleColliderUtility = movementStateMachine.MovementManager.CapsuleColliderUtility;
    }

    public override void FixedTick()
    {
        base.FixedTick();

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
            movementStateMachine.ReusableData.InAir = false;
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
            movementStateMachine.ReusableData.InAir = true;
            //movementStateMachine.MovementManager.Rigidbody.AddForce(movementStateMachine.MovementManager.Rigidbody.velocity.y * )
            //Debug.Log(movementStateMachine.MovementManager.Rigidbody.velocity);

            // !!! A METTRE DANS PLAYER AIR STATE
            //movementStateMachine.MovementManager.Metrics.CurrentPlayerSO.GroundedData.GravityModifier.Evaluate()
            movementStateMachine.MovementManager.Rigidbody.AddForce(Physics.gravity * movementStateMachine.MovementManager.Metrics.CurrentPlayerSO.GroundedData.GravityMultiplier, ForceMode.Acceleration);
        }
    }

    protected float SetSlopeSpeedModifierOnAngle(float angle)
    {
        float slopeSpeedModifier = metricsManager.CurrentPlayerSO.GroundedData.SlopeSpeedAngle.Evaluate(angle);

        movementStateMachine.ReusableData.MovementOnSlopeSpeedModifier = slopeSpeedModifier;

        return slopeSpeedModifier;
    }

    protected void AddVerticalForce(float distanceToGround, float forceMultiplier = 10f)
    {
        float amountToLift = distanceToGround * forceMultiplier - GetCurrentVerticalVelocity().y;

        Vector3 liftForce = new Vector3(0f, amountToLift, 0f);

        movementStateMachine.MovementManager.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
    }

    protected override void AddInputActionCallbacks()
    {
        base.AddInputActionCallbacks();

        movementStateMachine.MovementManager.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
    }

    protected override void RemoveInputActionCallbacks()
    {
        base.RemoveInputActionCallbacks();

        movementStateMachine.MovementManager.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
        movementStateMachine.ChangeState(movementStateMachine.IdleState);
    }

    /// <summary>
    /// Method use when the player change to a moving state
    /// </summary>
    protected virtual void OnMove()
    {
        if (movementStateMachine.ReusableData.ShouldWalk)
        {
            movementStateMachine.ChangeState(movementStateMachine.SlowState);
            return;
        }

        movementStateMachine.ChangeState(movementStateMachine.RunningState);
    }
}
