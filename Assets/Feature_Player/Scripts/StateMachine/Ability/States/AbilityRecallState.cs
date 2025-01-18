using UnityEngine;
using UnityEngine.UIElements;

public class AbilityRecallState : AbilityState
{
    public AbilityRecallState(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
    }

    public override void Tick()
    {
        base.Tick();
        
        if (reusableData.LeftInput)
        {
            reusableData.LeftObject.SetNewInfo(leftLauncher.position, metricsManager.CurrentPlayerSO.AbilityData.RecallSpeed, null);
            
            if (Vector3.Distance(reusableData.LeftObject.transform.position, leftLauncher.position) < metricsManager.CurrentPlayerSO.AbilityData.DistanceToEndRecall)
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
            }
        }
        
        if (reusableData.RightInput)
        {
            reusableData.RightObject.SetNewInfo(rightLauncher.position, metricsManager.CurrentPlayerSO.AbilityData.RecallSpeed, null);
            
            if (Vector3.Distance(reusableData.RightObject.transform.position, rightLauncher.position) < metricsManager.CurrentPlayerSO.AbilityData.DistanceToEndRecall)
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (reusableData.LeftInput)
        {
            GameObject.Destroy(reusableData.LeftObject.gameObject);
            reusableData.LeftObject = null;
            reusableData.LeftParent = null;
            leftLauncher.GetComponent<MeshRenderer>().enabled = true;
        }

        if (reusableData.RightInput)
        {
            GameObject.Destroy(reusableData.RightObject.gameObject);
            reusableData.RightObject = null;
            reusableData.RightParent = null;
            rightLauncher.GetComponent<MeshRenderer>().enabled = true;
        }
        
        reusableData.LeftInput = false;
        reusableData.RightInput = false;
    }
}
