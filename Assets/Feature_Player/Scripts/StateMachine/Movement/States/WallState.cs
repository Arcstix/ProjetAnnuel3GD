using UnityEngine;

public class WallState : MovementState
{
    public WallState(MovementStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Tick()
    {
        base.Tick();
        
        if (!reusableData.WallRight && !reusableData.WallLeft)
        {
            stateMachine.ChangeState(stateMachine.JumpState);
        }
    }
    
    public static Vector3 GetSlideDirection(Vector3 normal, Vector3 velocity, float maxAngle)
    {
        Vector3 tangent = Vector3.Cross(normal, Vector3.up);
        float signedAngle = Vector3.SignedAngle(normal, velocity.normalized, -Vector3.up);
        float multiplier = Mathf.Abs(signedAngle) >= maxAngle ? 0 : Mathf.Sign(signedAngle);
        
        return multiplier * tangent;
    }
}
