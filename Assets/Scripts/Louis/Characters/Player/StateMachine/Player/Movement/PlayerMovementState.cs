using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine movementStateMachine;

    protected PlayerGroundedData movementData;

    #region StateMachine Methods
    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
    {
        movementStateMachine = playerMovementStateMachine;

        movementData = playerMovementStateMachine.PlayerStateMachine.PlayerSO.GroundedData;
    }

    public virtual void Enter()
    {
        AddInputActionCallbacks();
    }


    public virtual void Exit()
    {
        RemoveInputActionCallbacks();
    }

    public virtual void Tick()
    {
        
    }

    public virtual void FixedTick()
    {
        Move();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    #endregion

    #region Main Methods
    private void ReadMovementInput()
    {
        movementStateMachine.ReusableData.MovementInput = movementStateMachine.PlayerStateMachine.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        if(movementStateMachine.ReusableData.MovementInput == Vector2.zero || movementStateMachine.ReusableData.MovementSpeedModifier == 0f) { return; }

        Vector3 movementDirection = GetMovementDirection();

        float targetRotationYAngle = HandleRotation(movementDirection);

        Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

        float movementSpeed = GetMovementSpeed();

        Vector3 currentHorizontalVelocity = GetCurrentHorizontalVelocity();       

        movementStateMachine.PlayerStateMachine.Rigidbody.AddForce(movementSpeed * targetRotationDirection - currentHorizontalVelocity, ForceMode.VelocityChange);
    }

    private float HandleRotation(Vector3 direction)
    {
        float directionAngle = UpdateTargetRotation(direction);

        RotateTowardsTargetRotation(directionAngle);
        return directionAngle;
    }

    private static float GetDirectionAngle(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        if (targetAngle < 0f)
        {
            targetAngle += 360f;
        }

        return targetAngle;
    }

    private float AddCameraRotationToAngle(float directionAngle)
    {
        directionAngle += movementStateMachine.PlayerStateMachine.Camera.transform.eulerAngles.y;

        if (directionAngle > 360f)
        {
            directionAngle -= 360f;
        }

        return directionAngle;
    }

    #endregion

    #region Reusable Methods
    protected Vector3 GetMovementDirection()
    {
        return new Vector3(movementStateMachine.ReusableData.MovementInput.x, 0f, movementStateMachine.ReusableData.MovementInput.y);
    }

    protected float GetMovementSpeed()
    {
        return movementData.BaseSpeed * movementStateMachine.ReusableData.MovementSpeedModifier * movementStateMachine.ReusableData.MovementOnSlopeSpeedModifier;
    }

    protected Vector3 GetCurrentHorizontalVelocity()
    {
        Vector3 playerHorizontalVelocity = movementStateMachine.PlayerStateMachine.Rigidbody.velocity;
        playerHorizontalVelocity.y = 0f;
        return playerHorizontalVelocity;
    }

    protected Vector3 GetCurrentVerticalVelocity()
    {
        Vector3 playerCurrentVerticalVelocity = new Vector3(0f, movementStateMachine.PlayerStateMachine.Rigidbody.velocity.y, 0f);
        return playerCurrentVerticalVelocity;
    }

    protected void RotateTowardsTargetRotation(float directionAngle)
    {
        float currentYAngle = movementStateMachine.PlayerStateMachine.transform.eulerAngles.y;

        if(currentYAngle == movementStateMachine.ReusableData.CurrentTargetRotation)
        {
            return;
        }

        float smoothYAngle = Mathf.SmoothDampAngle(currentYAngle, movementStateMachine.ReusableData.CurrentTargetRotation, ref movementStateMachine.ReusableData.TurnSmoothVelocity, movementStateMachine.ReusableData.TimeToReachTargetRotation - movementStateMachine.ReusableData.DampedTargetRotationPassedTime);

        movementStateMachine.ReusableData.DampedTargetRotationPassedTime += Time.deltaTime;
        movementStateMachine.PlayerStateMachine.Rigidbody.MoveRotation(Quaternion.Euler(0f, smoothYAngle, 0f));
    }

    protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
    {
        float directionAngle = GetDirectionAngle(direction);

        if (shouldConsiderCameraRotation)
        {
            directionAngle = AddCameraRotationToAngle(directionAngle);
        }       

        if (directionAngle != movementStateMachine.ReusableData.CurrentTargetRotation)
        {
            UpdateTargetRotationData(directionAngle);
        }

        return directionAngle;
    }

    private void UpdateTargetRotationData(float directionAngle)
    {
        movementStateMachine.ReusableData.CurrentTargetRotation = directionAngle;

        movementStateMachine.ReusableData.DampedTargetRotationPassedTime = 0;
    }

    protected Vector3 GetTargetRotationDirection(float targetAngle)
    {
        return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    }

    protected void ResetVelocity()
    {
        movementStateMachine.PlayerStateMachine.Rigidbody.velocity = Vector3.zero;
    }

    protected virtual void AddInputActionCallbacks()
    {
        movementStateMachine.PlayerStateMachine.Input.PlayerActions.SlowToggle.started += OnSlowStarted;
    }

    protected virtual void RemoveInputActionCallbacks()
    {
        movementStateMachine.PlayerStateMachine.Input.PlayerActions.SlowToggle.started -= OnSlowStarted;
    }

    #endregion

    #region Input Methods

    protected virtual void OnSlowStarted(InputAction.CallbackContext context)
    {
        movementStateMachine.ReusableData.ShouldWalk = !movementStateMachine.ReusableData.ShouldWalk;
    }

    #endregion
}
