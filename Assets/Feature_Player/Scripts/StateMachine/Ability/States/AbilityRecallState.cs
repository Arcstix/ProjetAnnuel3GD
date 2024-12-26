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
        
        // TODO : Method qui récupère la balle 
        if (reusableData.LeftInput)
        {
            reusableData.LeftObject.transform.position = Vector3.Lerp(reusableData.LeftObject.transform.position,
                leftLauncher.position, Time.deltaTime * metricsManager.CurrentPlayerSO.AbilityData.RecallSpeed);

            if (Vector3.Distance(reusableData.LeftObject.transform.position, leftLauncher.position) < 0.1f)
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
            }
        }
        
        if (reusableData.RightInput)
        {
            reusableData.RightObject.transform.position = Vector3.Lerp(reusableData.RightObject.transform.position,
                rightLauncher.position, Time.deltaTime * metricsManager.CurrentPlayerSO.AbilityData.RecallSpeed);

            if (Vector3.Distance(reusableData.RightObject.transform.position, rightLauncher.position) < 0.1f)
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
            GameObject.Destroy(reusableData.LeftObject);
            reusableData.LeftObject = null;
            leftLauncher.GetComponent<MeshRenderer>().enabled = true;
        }

        if (reusableData.RightInput)
        {
            GameObject.Destroy(reusableData.RightObject);
            reusableData.RightObject = null;
            rightLauncher.GetComponent<MeshRenderer>().enabled = true;
        }
        
        reusableData.LeftInput = false;
        reusableData.RightInput = false;
    }
}
