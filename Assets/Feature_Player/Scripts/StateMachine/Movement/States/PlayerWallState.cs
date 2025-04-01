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
    
    private bool AboveGround()
    {
        return !Physics.Raycast(rigidbody.transform.position, Vector3.down, wallData.MinJumpHeight, stateMachine.MovementManager.CapsuleUtility.LayerData.GroundLayer);
    }
}
