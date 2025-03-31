using UnityEngine;

public class PlayerWallState : PlayerMovementState
{
    protected WallData wallData;
    protected RaycastHit leftWallhit;
    protected RaycastHit rightWallhit;
    
    public PlayerWallState(PlayerMovementStateMachine playerStateMachine) : base(playerStateMachine)
    {
        wallData = metricsManager.CurrentMetrics.WallData;
    }

    public override void Tick()
    {
        base.Tick();
        
        CheckForWall();
    }
    
    private void CheckForWall()
    {
        reusableData.WallRight = Physics.Raycast(rigidbody.transform.position, rigidbody.transform.right, out rightWallhit, wallData.WallCheckDistance, wallData.WallLayer);
        reusableData.WallLeft = Physics.Raycast(rigidbody.transform.position, -rigidbody.transform.right, out leftWallhit, wallData.WallCheckDistance, wallData.WallLayer);
    }
    
    private bool AboveGround()
    {
        return !Physics.Raycast(rigidbody.transform.position, Vector3.down, wallData.MinJumpHeight, stateMachine.MovementManager.CapsuleUtility.LayerData.GroundLayer);
    }
}
