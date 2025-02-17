using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Se script sert � avoir toutes les m�thodes li�s au mouvement et � la rotation
/// </summary>
public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine stateMachine;
    protected PlayerMetricsManager metricsManager;
    protected PlayerInput input;
    protected PlayerCameraManager cameraManager;
    protected PlayerReusableStateData reusableData;
    protected Rigidbody rigidbody;

    #region StateMachine Methods

    /// <summary>
    /// Un constructeur poss�dant des raccourcies utiles pour le code. ATTENTION A NE PAS SPECIFIER DES COMPOSANTS SUSCEPTIBLE DE CHANGER
    /// </summary>
    /// <param name="playerStateMachine"></param>
    public PlayerMovementState(PlayerMovementStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        metricsManager = playerStateMachine.MovementManager.Metrics;
        input = stateMachine.MovementManager.Input;
        reusableData = stateMachine.ReusableData;
        rigidbody = stateMachine.MovementManager.Rb;

        if (stateMachine.MovementManager.CameraManager != null)
        {
            cameraManager = stateMachine.MovementManager.CameraManager;
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
        
    }

    public virtual void FixedTick()
    {
        if (reusableData.CanMove && !reusableData.OnTransportation)
        {
            Move();
        }
    }

    public virtual void HandleInput()
    {
        if (reusableData.CanMove && !reusableData.OnTransportation)
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

        rigidbody.AddForce(targetRotationDirection * (movementSpeed * metricsManager.ExternForce) - currentHorizontalVelocity, ForceMode.VelocityChange);
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
        directionAngle += stateMachine.MovementManager.Cam.transform.eulerAngles.y;

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
        return metricsManager.CurrentMetrics.GroundedData.BaseSpeed * reusableData.MovementSpeedModifier * reusableData.MovementOnSlopeSpeedModifier;
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
        float currentYAngle = stateMachine.MovementManager.transform.eulerAngles.y;

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
