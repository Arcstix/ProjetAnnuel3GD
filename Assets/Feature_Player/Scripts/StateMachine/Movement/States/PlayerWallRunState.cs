using UnityEngine;

public class PlayerWallRunState : PlayerWallState
{
    public PlayerWallRunState(PlayerMovementStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        rigidbody.useGravity = false;
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
    }

    public override void FixedTick()
    {
        base.FixedTick();
        
        Vector3 wallNormal = reusableData.WallRight ? rightWallhit.normal : leftWallhit.normal;
        
        Vector3 slideDirection = GetSlideDirection(wallNormal, rigidbody.velocity, metricsManager.CurrentMetrics.WallData.MaxAngle);

        if (slideDirection != Vector3.zero)
        {
            WallRunningMovement(wallNormal, slideDirection);
        }
        else
        {
            stateMachine.ChangeState(stateMachine.FallingState);
        }
        
    }

    private void WallRunningMovement(Vector3 wallNormal, Vector3 slideDirection)
    {
        // forward force
        rigidbody.AddForce(slideDirection * wallData.WallRunForce - GetCurrentHorizontalVelocity(), ForceMode.VelocityChange);
            
        // push to wall force
        if (!(reusableData.WallLeft && reusableData.MovementInput.x > 0) && !(reusableData.WallRight && reusableData.MovementInput.x < 0))
            rigidbody.AddForce(-wallNormal * 100, ForceMode.Force);
    }

}
