using System;
using UnityEngine;

public class AbilityThrowState : AbilityState
{
    public event Action OnThrow;

    private Vector3 direction;
    
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
        
        AimThrow();
        
        _stateMachine.ChangeState(_stateMachine.RecallState);
    }

    private void AimBotThrow()
    {
        
    }

    private void AimThrow()
    {
        Vector3 targetPoint = _stateMachine.AbilityManager.Cam.transform.position + _stateMachine.AbilityManager.Cam.transform.forward * 20;
        
        if (reusableData.LeftThrow)
        {
            Vector3 leftPos = leftProjectileLauncher.transform.position;
            
            direction = (targetPoint - leftPos).normalized;
            // We Throw Left Projectile
            reusableData.LeftObject.ThrowProjectile(direction, metricsManager.CurrentMetrics.AbilityData.ThrowSpeed);
            targetSystem.leftTargetLaunch = null;
        }
        else
        {
            Vector3 rightPos = rightProjectileLauncher.transform.position;
            
            direction = (targetPoint - rightPos).normalized;
            // We Throw Right Projectile
            reusableData.RightObject.ThrowProjectile(direction, metricsManager.CurrentMetrics.AbilityData.ThrowSpeed);
            targetSystem.rightTargetLaunch = null;
        }
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
