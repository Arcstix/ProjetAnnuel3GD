using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityIdleState : AbilityState
{
    public AbilityIdleState(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        AddInputRightThrowRecall();
        AddInputLeftThrowRecall();
        AddInputLeftAttraction();
        AddInputRightAttraction();
    }

    public override void Exit()
    {
        base.Exit();
        RemoveInputRightThrowRecall();
        RemoveInputLeftThrowRecall();
        RemoveInputRightAttraction();
        RemoveInputLeftAttraction();
    }
}
