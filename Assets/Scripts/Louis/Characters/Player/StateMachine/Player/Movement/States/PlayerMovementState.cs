using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine movementStateMachine;
    protected Vector2 movementInput;

    protected float baseSpeed = 5f;
    protected float speedModifier = 1f;
    protected float turnSmoothTime = 0.1f;
    
    private float turnSmoothVelocity;
    private float currentTargetRotation;
    private float dampedTargetRotationPassedTime;

    protected bool shooldSlow;

    #region StateMachine Methods
    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
    {
        movementStateMachine = playerMovementStateMachine;

        InitializeData();
    }

    private void InitializeData()
    {

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
        movementInput = movementStateMachine.PlayerStateMachine.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        if(movementInput == Vector2.zero || speedModifier == 0f) { return; }

        Debug.Log("Moving");
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
        return new Vector3(movementInput.x, 0f, movementInput.y);
    }

    protected float GetMovementSpeed()
    {
        return baseSpeed * speedModifier;
    }

    protected Vector3 GetCurrentHorizontalVelocity()
    {
        Vector3 playerHorizontalVelocity = movementStateMachine.PlayerStateMachine.Rigidbody.velocity;
        playerHorizontalVelocity.y = 0f;
        return playerHorizontalVelocity;
    }

    protected void RotateTowardsTargetRotation(float directionAngle)
    {
        float currentYAngle = movementStateMachine.PlayerStateMachine.transform.eulerAngles.y;

        if(currentYAngle == currentTargetRotation)
        {
            return;
        }

        float smoothYAngle = Mathf.SmoothDampAngle(currentYAngle, currentTargetRotation, ref turnSmoothVelocity, turnSmoothTime - dampedTargetRotationPassedTime);

        dampedTargetRotationPassedTime += Time.deltaTime;
        movementStateMachine.PlayerStateMachine.Rigidbody.MoveRotation(Quaternion.Euler(0f, smoothYAngle, 0f));
    }

    protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
    {
        float directionAngle = GetDirectionAngle(direction);

        if (shouldConsiderCameraRotation)
        {
            directionAngle = AddCameraRotationToAngle(directionAngle);
        }       

        if (directionAngle != currentTargetRotation)
        {
            UpdateTargetRotationData(directionAngle);
        }

        return directionAngle;
    }

    private void UpdateTargetRotationData(float directionAngle)
    {
        currentTargetRotation = directionAngle;

        dampedTargetRotationPassedTime = 0;
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
        shooldSlow = !shooldSlow;
    }

    #endregion
}
