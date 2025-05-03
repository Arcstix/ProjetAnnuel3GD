using System;
using UnityEngine;

public class AbilityRecallBothObject : AbilityState
{
    public event Action OnRightRecall;
    public event Action OnLeftRecall;
    
    
    public AbilityRecallBothObject(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        OnLeftRecall?.Invoke();
        if (reusableData.LeftParent != null)
        {
            if (reusableData.LeftObject != null)
            {
                reusableData.LeftObject.DisableInteraction();
            }
            targetSystem.leftTargetLaunch = null;
        }

        OnRightRecall?.Invoke();
        if (reusableData.RightParent != null)
        {
            if (reusableData.RightObject != null)
            {
                reusableData.RightObject.DisableInteraction();
            }
                
            targetSystem.rightTargetLaunch = null;
        }
    }
    
    public override void Tick()
    {
        base.Tick();
        
        if (reusableData.LeftObject)
        {
            reusableData.LeftObject.SetNewInfo(leftLauncher.position, metricsManager.CurrentMetrics.AbilityData.RecallSpeed, null);
                
            if (Vector3.Distance(reusableData.LeftObject.transform.position, leftLauncher.position) < metricsManager.CurrentMetrics.AbilityData.DistanceToEndRecall)
            {
                GameObject.Destroy(reusableData.LeftObject.gameObject);
            }
        }
            
        if (reusableData.RightObject)
        {
            reusableData.RightObject.SetNewInfo(rightLauncher.position, metricsManager.CurrentMetrics.AbilityData.RecallSpeed, null);
                
            if (Vector3.Distance(reusableData.RightObject.transform.position, rightLauncher.position) < metricsManager.CurrentMetrics.AbilityData.DistanceToEndRecall)
            {
                GameObject.Destroy(reusableData.RightObject.gameObject);
            }
        }

        if (reusableData.LeftObject == null && reusableData.RightObject == null)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        if (reusableData.LeftObject)
        {
            GameObject.Destroy(reusableData.LeftObject.gameObject);
        }
        reusableData.LeftObject = null;
        reusableData.LeftParent = null;
        targetSystem.leftTargetLaunch = null;
        leftLauncher.GetComponent<MeshRenderer>().enabled = true;

        if (reusableData.RightObject)
        {
            GameObject.Destroy(reusableData.RightObject.gameObject);
        }
        reusableData.RightObject = null;
        reusableData.RightParent = null;
        targetSystem.rightTargetLaunch = null;
        rightLauncher.GetComponent<MeshRenderer>().enabled = true;
        reusableData.LeftInput = false;
        reusableData.RightInput = false;
    }
}
