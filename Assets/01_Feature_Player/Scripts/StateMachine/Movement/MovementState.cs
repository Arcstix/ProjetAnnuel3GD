using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Se script sert � avoir toutes les m�thodes li�s au mouvement et � la rotation
/// </summary>
public class MovementState : IState
{
    protected MovementStateMachine stateMachine;
    protected PlayerMetricsManager metricsManager;
    protected PlayerInput input;
    protected PlayerCameraManager cameraManager;
    protected PlayerReusableStateData reusableData;
    protected Rigidbody rigidbody;
    protected WallData wallData;
    protected RaycastHit leftWallhit;
    protected RaycastHit rightWallhit;
    
    protected RaycastHit cashWallhit;
    
    private CapsuleColliderUtility capsuleColliderUtility;

    protected InputAction movement;
    protected InputAction jump;
    protected InputAction walkToggle;

    #region StateMachine Methods

    /// <summary>
    /// Un constructeur poss�dant des raccourcies utiles pour le code. ATTENTION A NE PAS SPECIFIER DES COMPOSANTS SUSCEPTIBLE DE CHANGER
    /// </summary>
    /// <param name="stateMachine"></param>
    public MovementState(MovementStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        metricsManager = stateMachine.MovementManager.Metrics;
        input = stateMachine.MovementManager.Input;
        reusableData = this.stateMachine.ReusableData;
        rigidbody = this.stateMachine.MovementManager.Rb;

        if (this.stateMachine.MovementManager.CameraManager != null)
        {
            cameraManager = this.stateMachine.MovementManager.CameraManager;
        }
        
        capsuleColliderUtility = stateMachine.MovementManager.CapsuleUtility;
        wallData = metricsManager.CurrentMetrics.WallData;
        
        movement = input.actions["Movement"];
        jump = input.actions["Jump"];
        walkToggle = input.actions["WalkToggle"];
    }

    public virtual void Enter()
    {
    }


    public virtual void Exit()
    {
    }

    public virtual void Tick()
    {
        if (jump.WasPressedThisFrame() && stateMachine.currentState != stateMachine.FallingState &&
            stateMachine.currentState != stateMachine.SoftLandingState && stateMachine.currentState != stateMachine.JumpState)
        {
            stateMachine.ChangeState(stateMachine.JumpState);
        }

        if (stateMachine.currentState != stateMachine.TransportedState && reusableData.OnTransportation)
        {
            stateMachine.ChangeState(stateMachine.TransportedState);
        }
    }

    public virtual void FixedTick()
    {
        if (reusableData.CanMove && !reusableData.OnTransportation)
        {
            Move();
        }
        
        CheckForWall();
    }

    public virtual void HandleInput()
    {
        if (reusableData.CanMove && !reusableData.OnTransportation)
        {
            ReadMovementInput();
        }
    }
    
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Collision Enter");
            reusableData.OnWall = collision.gameObject;
            if (stateMachine.MovementManager.WallCheck.CanWallRun())
            {
                Debug.Log("Wall Run");
                stateMachine.ChangeState(stateMachine.WallRunState);
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (reusableData.OnWall != null && collision.gameObject == reusableData.OnWall)
        {
            reusableData.OnWall = null;
        }
    }

    #endregion

    #region Main Methods
    private void ReadMovementInput()
    {
        reusableData.MovementInput = movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        if (reusableData.MovementInput == Vector2.zero || reusableData.MovementSpeedModifier == 0f) { return; }

        Vector3 movementDirection = GetMovementDirection();

        float targetRotationYAngle = HandleRotation(movementDirection);

        Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

        float movementSpeed = GetMovementSpeed();

        Vector3 currentHorizontalVelocity = GetCurrentHorizontalVelocity();

        if (reusableData.InAir)
        {
            rigidbody.AddForce(Vector3.Lerp(currentHorizontalVelocity, targetRotationDirection * (movementSpeed * metricsManager.ExternForce) - currentHorizontalVelocity, 1f) , ForceMode.Acceleration);
        }
        else
        {
            rigidbody.AddForce(Vector3.Lerp(currentHorizontalVelocity, targetRotationDirection * (movementSpeed * metricsManager.ExternForce) - currentHorizontalVelocity, 1f) , ForceMode.VelocityChange);
        }
        
    }

    protected float HandleRotation(Vector3 direction)
    {
        if (GetMovementDirection() == Vector3.zero) return reusableData.CurrentTargetRotation;
        
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
        
        if(Mathf.Approximately(currentYAngle, reusableData.CurrentTargetRotation))
        {
            return;
        }

        float timeToReachTargetRotation = metricsManager.CurrentMetrics.GroundedData.TimeToReachTargetRotation;
        float smoothYAngle = Mathf.SmoothDampAngle(currentYAngle, reusableData.CurrentTargetRotation, ref reusableData.TurnSmoothVelocity, timeToReachTargetRotation - reusableData.DampedTargetRotationPassedTime);

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

        if (!Mathf.Approximately(directionAngle, reusableData.CurrentTargetRotation))
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
        if (rigidbody.velocity.sqrMagnitude > 0f)
        {
            Vector3 velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            rigidbody.velocity = Vector3.Lerp(velocity, Vector3.zero, 0.1f);
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }
    
    protected void CheckForWall()
    {
        Vector3 capsuleColliderCenterInWorldSpace = capsuleColliderUtility.CapsuleColliderData.Collider.bounds.center;
        
        reusableData.WallRight = Physics.Raycast(capsuleColliderCenterInWorldSpace, rigidbody.transform.right, out rightWallhit, wallData.WallCheckDistance, wallData.WallLayer);
        reusableData.WallLeft = Physics.Raycast(capsuleColliderCenterInWorldSpace, -rigidbody.transform.right, out leftWallhit, wallData.WallCheckDistance, wallData.WallLayer);

        if (rightWallhit.collider != null)
        {
            cashWallhit = rightWallhit;
            return;
        }

        if (leftWallhit.collider != null)
        {
            cashWallhit = leftWallhit;
        }
    }

    protected virtual void SubscribeInputAction()
    {
        walkToggle.started += OnSlowStarted;
    }

    protected virtual void UnsubscribeInputAction()
    {
        walkToggle.started -= OnSlowStarted;
    }

    #endregion

    #region Input Methods

    protected virtual void OnSlowStarted(InputAction.CallbackContext context)
    {
        reusableData.ShouldWalk = !reusableData.ShouldWalk;
    }

    #endregion
}
