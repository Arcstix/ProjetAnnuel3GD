using UnityEngine;

public class PlayerWallRunState : PlayerWallState
{
    public PlayerWallRunState(PlayerMovementStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void FixedTick()
    {
        base.FixedTick();
        
        WallRunningMovement();
    }

    private void WallRunningMovement()
    {
        rigidbody.useGravity = false;
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);

        Vector3 wallNormal = reusableData.WallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, rigidbody.transform.up);

        if ((rigidbody.transform.forward - wallForward).magnitude > (rigidbody.transform.forward - -wallForward).magnitude)
            wallForward = -wallForward;

        // forward force
        rigidbody.AddForce(wallForward * wallData.WallRunForce, ForceMode.Force);

        // // upwards/downwards force
        // if (upwardsRunning)
        //     rb.velocity = new Vector3(rb.velocity.x, wallClimbSpeed, rb.velocity.z);
        // if (downwardsRunning)
        //     rb.velocity = new Vector3(rb.velocity.x, -wallClimbSpeed, rb.velocity.z);

        // push to wall force
        if (!(reusableData.WallLeft && reusableData.MovementInput.x > 0) && !(reusableData.WallRight && reusableData.MovementInput.x < 0))
            rigidbody.AddForce(-wallNormal * 100, ForceMode.Force);
    }

}
