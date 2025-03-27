using System;
using UnityEngine;

public class AbilityThrowState : AbilityState
{
    public event Action OnThrow;
    
    public AbilityThrowState(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        
        OnThrow?.Invoke();
        
        if (reusableData.LeftThrow)
        {
            if (reusableData.LeftParent != null)
            {
                reusableData.LeftObject.DisableInteraction();
            }
        }

        if (reusableData.RightThrow)
        {
            if (reusableData.RightParent != null)
            {
                reusableData.RightObject.DisableInteraction();
            }
        }
    }

    public override void FixedTick()
    {
        base.FixedTick();
        
        Vector3 direction = (aimTransform.position - _stateMachine.AbilityManager.Cam.transform.position).normalized;
        
        if (reusableData.LeftThrow)
        {
            // We Throw Left Projectile
            reusableData.LeftObject.ThrowProjectile(direction, metricsManager.CurrentMetrics.AbilityData.ThrowSpeed);
        }

        if (reusableData.RightThrow)
        {
            // We Throw Right Projectile
            reusableData.RightObject.ThrowProjectile(direction, metricsManager.CurrentMetrics.AbilityData.ThrowSpeed);
        }
        
        _stateMachine.ChangeState(_stateMachine.RecallState);
    }

    public override void Exit()
    {
        base.Exit();

        reusableData.LeftInput = reusableData.LeftThrow;
        reusableData.RightInput = reusableData.RightThrow;

        reusableData.LeftThrow = false;
        reusableData.RightThrow = false;
    }
}
