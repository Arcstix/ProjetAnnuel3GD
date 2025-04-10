using System;
using UnityEngine;

public class OnPlatformState : GroundedState
{
    public event Action OnPlatform; 
    
    public OnPlatformState(MovementStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        OnPlatform?.Invoke();
        rigidbody.velocity = Vector3.zero;
        rigidbody.isKinematic = true;
    }

    public override void Tick()
    {
        base.Tick();
        
        if (Vector3.Distance(rigidbody.transform.position, reusableData.TargetPosition) > 0.2f)
        {
            rigidbody.transform.position =
                Vector3.Lerp(rigidbody.transform.position, reusableData.TargetPosition, 0.1f);
        }
    }

    public override void Exit()
    {
        base.Exit();
        rigidbody.isKinematic = false;
    }
}
