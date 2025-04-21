using System;
using UnityEngine;
using UnityEngine.UIElements;

public class AbilityRecallState : AbilityState
{
    public event Action OnRightRecall;
    public event Action OnLeftRecall;
    
    
    public AbilityRecallState(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        if (reusableData.LeftInput)
        {
            OnLeftRecall?.Invoke();
            if (reusableData.LeftParent != null)
            {
                if (reusableData.LeftObject != null)
                {
                    reusableData.LeftObject.DisableInteraction();
                }
                targetSystem.leftTargetLaunch = null;
            }
        }

        if (reusableData.RightInput)
        {
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
        
        Debug.Log("Enter recall");
    }
    
    public override void Tick()
    {
        base.Tick();
        
        if (reusableData.LeftInput && reusableData.RightInput)
        {
            Debug.Log("tick recall");
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

        if (!reusableData.RightInput || !reusableData.LeftInput)
        {
            if (reusableData.LeftInput)
            {
                if (reusableData.LeftObject)
                {
                    reusableData.LeftObject.SetNewInfo(leftLauncher.position, metricsManager.CurrentMetrics.AbilityData.RecallSpeed, null);
                }
                else
                {
                    _stateMachine.ChangeState(_stateMachine.IdleState);
                    return;
                }
            
                if (Vector3.Distance(reusableData.LeftObject.transform.position, leftLauncher.position) < metricsManager.CurrentMetrics.AbilityData.DistanceToEndRecall)
                {
                    _stateMachine.ChangeState(_stateMachine.IdleState);
                }
            }
        
            if (reusableData.RightInput)
            {
                if (reusableData.RightObject)
                {
                    reusableData.RightObject.SetNewInfo(rightLauncher.position, metricsManager.CurrentMetrics.AbilityData.RecallSpeed, null);
                }
                else
                {
                    _stateMachine.ChangeState(_stateMachine.IdleState);
                    return;
                }
            
                if (Vector3.Distance(reusableData.RightObject.transform.position, rightLauncher.position) < metricsManager.CurrentMetrics.AbilityData.DistanceToEndRecall)
                {
                    _stateMachine.ChangeState(_stateMachine.IdleState);
                }
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("exit recall");
        
        if (reusableData.LeftInput)
        {
            if (reusableData.LeftObject)
            {
                GameObject.Destroy(reusableData.LeftObject.gameObject);
            }
            reusableData.LeftObject = null;
            reusableData.LeftParent = null;
            targetSystem.leftTargetLaunch = null;
            leftLauncher.GetComponent<MeshRenderer>().enabled = true;
        }

        if (reusableData.RightInput)
        {
            if (reusableData.RightObject)
            {
                GameObject.Destroy(reusableData.RightObject.gameObject);
            }
            reusableData.RightObject = null;
            reusableData.RightParent = null;
            targetSystem.rightTargetLaunch = null;
            rightLauncher.GetComponent<MeshRenderer>().enabled = true;
        }
        
        reusableData.LeftInput = false;
        reusableData.RightInput = false;
    }
}
