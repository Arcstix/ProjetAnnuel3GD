using UnityEngine;

public class AbilityShootState : AbilityState
{
    public AbilityShootState(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        // TODO : Method Shoot mettre la balle à l'endroit désigné et si pas d'endroit désigné tiré un Raycast
        
        _stateMachine.ChangeState(_stateMachine.IdleState);
        return;
    }
}
