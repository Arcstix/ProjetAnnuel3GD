using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Se script sert à avoir toutes les méthodes liés au mouvement et à la rotation
/// </summary>
public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine movementStateMachine;
    protected PlayerMetricsManager metricsManager;
    protected PlayerInput input;
    protected PlayerCameraManager cameraManager;
    protected PlayerReusableStateData reusableData;
    protected Rigidbody rigidbody;

    #region StateMachine Methods

    /// <summary>
    /// Un constructeur possédant des raccourcies utiles pour le code. ATTENTION A NE PAS SPECIFIER DES COMPOSANTS SUSCEPTIBLE DE CHANGER
    /// </summary>
    /// <param name="playerMovementStateMachine"></param>
    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
    {
        movementStateMachine = playerMovementStateMachine;
        metricsManager = playerMovementStateMachine.MovementManager.Metrics;
        input = movementStateMachine.MovementManager.Input;
        reusableData = movementStateMachine.ReusableData;
        rigidbody = movementStateMachine.MovementManager.Rigidbody;

        if (movementStateMachine.MovementManager.CameraManager != null)
        {
            cameraManager = movementStateMachine.MovementManager.CameraManager;
        }
    }

    public virtual void Enter()
    {
        SubscribeInputAction();          
    }


    public virtual void Exit()
    {
        UnsubscribeInputAction();
    }

    public virtual void Tick()
    {
        if (reusableData.InAir)
        {
            if (!reusableData.OnTransportation)
            {
                if (movementStateMachine.currentState != movementStateMachine.FallingState)
                {
                    movementStateMachine.ChangeState(movementStateMachine.FallingState);
                    return;
                }
            }
            else
            {
                if(movementStateMachine.currentState != movementStateMachine.IdleState)
                {
                    movementStateMachine.ChangeState(movementStateMachine.IdleState);
                    return;
                }
            }
        }
        else
        {
            if(!reusableData.CanMove && movementStateMachine.currentState != movementStateMachine.IdleState)
            {
                movementStateMachine.ChangeState(movementStateMachine.IdleState);
                return;
            }
        }
    }

    public virtual void FixedTick()
    {
        if (reusableData.CanMove)
        {
            Move();
        }
    }

    public virtual void HandleInput()
    {
        if (reusableData.CanMove)
        {
            ReadMovementInput();
        }
    }

    #endregion

    #region Main Methods
    private void ReadMovementInput()
    {
        reusableData.MovementInput = input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        if(reusableData.MovementInput == Vector2.zero || reusableData.MovementSpeedModifier == 0f) { return; }

        Vector3 movementDirection = GetMovementDirection();

        float targetRotationYAngle = HandleRotation(movementDirection);

        Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

        float movementSpeed = GetMovementSpeed();

        Vector3 currentHorizontalVelocity = GetCurrentHorizontalVelocity();       

        rigidbody.AddForce(movementSpeed * targetRotationDirection - currentHorizontalVelocity, ForceMode.VelocityChange);
    }

    public float HandleRotation(Vector3 direction)
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
        directionAngle += movementStateMachine.MovementManager.Camera.transform.eulerAngles.y;

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
        return new Vector3(reusableData.MovementInput.x, 0f, reusableData.MovementInput.y);
    }

    protected float GetMovementSpeed()
    {
        return metricsManager.CurrentPlayerSO.GroundedData.BaseSpeed * reusableData.MovementSpeedModifier * reusableData.MovementOnSlopeSpeedModifier;
    }

    protected Vector3 GetCurrentHorizontalVelocity()
    {
        Vector3 playerHorizontalVelocity = rigidbody.velocity;
        playerHorizontalVelocity.y = 0f;
        return playerHorizontalVelocity;
    }

    protected Vector3 GetCurrentVerticalVelocity()
    {
        Vector3 playerCurrentVerticalVelocity = new Vector3(0f, rigidbody.velocity.y, 0f);
        return playerCurrentVerticalVelocity;
    }

    protected void RotateTowardsTargetRotation(float directionAngle)
    {
        float currentYAngle = movementStateMachine.MovementManager.transform.eulerAngles.y;

        if(currentYAngle == reusableData.CurrentTargetRotation)
        {
            return;
        }

        float smoothYAngle = Mathf.SmoothDampAngle(currentYAngle, reusableData.CurrentTargetRotation, ref reusableData.TurnSmoothVelocity, reusableData.TimeToReachTargetRotation - reusableData.DampedTargetRotationPassedTime);

        reusableData.DampedTargetRotationPassedTime += Time.deltaTime;
        rigidbody.MoveRotation(Quaternion.Euler(0f, smoothYAngle, 0f));
    }

    protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
    {
        float directionAngle = GetDirectionAngle(direction);

        if (shouldConsiderCameraRotation)
        {
            directionAngle = AddCameraRotationToAngle(directionAngle);
        }

        if (directionAngle != reusableData.CurrentTargetRotation)
        {
            UpdateTargetRotationData(directionAngle);
        }

        return directionAngle;
    }

    private void UpdateTargetRotationData(float directionAngle)
    {
        reusableData.CurrentTargetRotation = directionAngle;

        reusableData.DampedTargetRotationPassedTime = 0;
    }

    protected Vector3 GetTargetRotationDirection(float targetAngle)
    {
        return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    }

    protected void ResetVelocity()
    {
        rigidbody.velocity = Vector3.zero;
    }

    protected virtual void SubscribeInputAction()
    {
        input.PlayerActions.WalkToggle.started += OnSlowStarted;
    }

    protected virtual void UnsubscribeInputAction()
    {
        input.PlayerActions.WalkToggle.started -= OnSlowStarted;
    }

    #endregion

    #region Input Methods

    protected virtual void OnSlowStarted(InputAction.CallbackContext context)
    {
        reusableData.ShouldWalk = !reusableData.ShouldWalk;
    }

    #endregion
}
