using System;
using DG.Tweening;
using UnityEngine;

public class WallRunState : WallState
{
    private Vector3 wallNormal;
    private Vector3 slideDirection;

    public event Action<float> OnWallRun;
    public event Action ExitWallRun;
    
    public event Action<int> EnterWallRun;
    
    public WallRunState(MovementStateMachine stateMachine) : base(stateMachine)
    {
    }


    public override void Enter()
    {
        base.Enter();

        reusableData.NumberOfConsecutiveWallRun++;
        EnterWallRun?.Invoke(reusableData.NumberOfConsecutiveWallRun);
        
        Debug.Log("Enter WallRunState");
        rigidbody.useGravity = false;
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
        
        if (reusableData.WallRight)
        {
            OnWallRun?.Invoke(metricsManager.CurrentMetrics.WallData.Inclinaison);
        }
        else
        {
            OnWallRun?.Invoke(-metricsManager.CurrentMetrics.WallData.Inclinaison);
        }
    }

    public override void FixedTick()
    {
        CheckForWall();
        
        wallNormal = reusableData.WallRight ? rightWallhit.normal : leftWallhit.normal;
        
        slideDirection = GetSlideDirection(wallNormal, rigidbody.velocity, metricsManager.CurrentMetrics.WallData.MaxAngle);

        if (slideDirection != Vector3.zero)
        {
            WallRunningMovement();
        }
        else
        {
            stateMachine.ChangeState(stateMachine.FallingState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        // forward force
        rigidbody.AddForce(slideDirection * wallData.WallRunForce - GetCurrentHorizontalVelocity(), ForceMode.VelocityChange);
        ExitWallRun?.Invoke();
    }

    private void WallRunningMovement()
    {
        // forward force
        rigidbody.AddForce(slideDirection * wallData.WallRunForce - GetCurrentHorizontalVelocity(), ForceMode.VelocityChange);
            
        // push to wall force
        if (!(reusableData.WallLeft && reusableData.MovementInput.x > 0) && !(reusableData.WallRight && reusableData.MovementInput.x < 0))
            rigidbody.AddForce(-wallNormal * 100, ForceMode.Force);
    }

}
