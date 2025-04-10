using UnityEngine;

public class AbilityMoveBothObject : AbilityTransportState
{
    
    public AbilityMoveBothObject(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        rightInteraction = reusableData.RightObject.GetComponent<InteractionSystem>();
        leftInteraction = reusableData.LeftObject.GetComponent<InteractionSystem>();
        reusableData.OnTransportation = false;
    }

    public override void FixedTick()
    {
        base.FixedTick();
        
        MoveBothObject();
    }

    public override void Exit()
    {
        base.Exit();
        
        MoveRightObjectToLaunch();
        MoveLeftObjectToLaunch();
    }

    private void MoveBothObject()
    {
        rightInteraction.Interactable(true);
                
        // Move LeftParent to RightParent
        reusableData.LeftObject.Move(reusableData.LeftParent, reusableData.RightParent);
        
        leftInteraction.Interactable(true);
        // Move RightParent to LeftParent
        reusableData.RightObject.Move(reusableData.RightParent, reusableData.LeftParent);
    }
}
