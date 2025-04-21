using UnityEngine;

public class WallRunState : WallState
{
    private Vector3 wallNormal;
    private Vector3 slideDirection;
    
    public WallRunState(MovementStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Enter WallRunState");
        rigidbody.useGravity = false;
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
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
