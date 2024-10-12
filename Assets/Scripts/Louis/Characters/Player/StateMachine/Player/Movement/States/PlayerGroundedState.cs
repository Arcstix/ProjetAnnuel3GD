using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerMovementState
{
    private CapsuleColliderUtility capsuleColliderUtility;

    public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        capsuleColliderUtility = movementStateMachine.PlayerStateMachine.CapsuleColliderUtility;
    }

    public override void FixedTick()
    {
        base.FixedTick();

        CheckDistanceToTheGround();
    }

    private void CheckDistanceToTheGround()
    {
        Vector3 capsuleColliderCenterInWorldSpace = capsuleColliderUtility.CapsuleColliderData.Collider.bounds.center;

        Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

        if(Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, capsuleColliderUtility.SlopeData.DistanceGroundCheck, capsuleColliderUtility.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
        {
            float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

            float floatSpeedModifier = SetSlopeSpeedModifierOnAngle(groundAngle);

            if(floatSpeedModifier == 0f)
            {
                return;
            }

            float distanceToGround = capsuleColliderUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y * movementStateMachine.PlayerStateMachine.transform.localScale.y - hit.distance;

            Debug.Log(distanceToGround);

            if (distanceToGround == 0f)
            {
                return;
            }

            AddVerticalForce(distanceToGround, capsuleColliderUtility.SlopeData.StepReachForce);
        }
    }

    private float SetSlopeSpeedModifierOnAngle(float angle)
    {
        float slopeSpeedModifier = movementData.SlopeSpeedAngle.Evaluate(angle);

        movementStateMachine.ReusableData.MovementOnSlopeSpeedModifier = slopeSpeedModifier;

        return slopeSpeedModifier;
    }

    private void AddVerticalForce(float distanceToGround, float forceMultiplier = 10f)
    {
        float amountToLift = distanceToGround * forceMultiplier - GetCurrentVerticalVelocity().y;

        Vector3 liftForce = new Vector3(0f, amountToLift, 0f);

        movementStateMachine.PlayerStateMachine.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
    }

    protected override void AddInputActionCallbacks()
    {
        base.AddInputActionCallbacks();

        movementStateMachine.PlayerStateMachine.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
    }

    protected override void RemoveInputActionCallbacks()
    {
        base.RemoveInputActionCallbacks();

        movementStateMachine.PlayerStateMachine.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
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
