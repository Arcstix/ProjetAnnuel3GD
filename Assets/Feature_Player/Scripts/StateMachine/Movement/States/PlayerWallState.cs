using UnityEngine;

public class PlayerWallState : PlayerMovementState
{
    protected WallData wallData;
    protected RaycastHit leftWallhit;
    protected RaycastHit rightWallhit;
    
    private CapsuleColliderUtility capsuleColliderUtility;
    
    public PlayerWallState(PlayerMovementStateMachine playerStateMachine) : base(playerStateMachine)
    {
        capsuleColliderUtility = stateMachine.MovementManager.CapsuleUtility;
        wallData = metricsManager.CurrentMetrics.WallData;
    }
    
    public override void Tick()
    {
        base.Tick();
        
        CheckForWall();
        
        if (!reusableData.WallRight && !reusableData.WallLeft)
        {
            stateMachine.ChangeState(stateMachine.FallingState);
        }
    }
    
    private void CheckForWall()
    {
        Vector3 capsuleColliderCenterInWorldSpace = capsuleColliderUtility.CapsuleColliderData.Collider.bounds.center;
        
        reusableData.WallRight = Physics.Raycast(capsuleColliderCenterInWorldSpace, Vector3.right, out rightWallhit, wallData.WallCheckDistance, wallData.WallLayer);
        reusableData.WallLeft = Physics.Raycast(capsuleColliderCenterInWorldSpace, -Vector3.right, out leftWallhit, wallData.WallCheckDistance, wallData.WallLayer);
    }
    
    public static Vector3 GetSlideDirection(Vector3 normal, Vector3 velocity, float maxAngle)
    {
        Vector3 tangent = Vector3.Cross(normal, Vector3.up);
        float signedAngle = Vector3.SignedAngle(normal, velocity.normalized, -Vector3.up);
        float multiplier = Mathf.Abs(signedAngle) >= maxAngle ? 0 : Mathf.Sign(signedAngle);
        
        return multiplier * tangent;
    }
}
