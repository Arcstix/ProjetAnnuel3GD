using UnityEngine;
using UnityEngine.UIElements;

public class AbilityRecallState : AbilityState
{
    public AbilityRecallState(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        // TODO : Method qui récupère la balle 
        
        _stateMachine.ChangeState(_stateMachine.IdleState);
        return;
    }
}
