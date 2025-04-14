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

        if (rightInteraction.interactorType != InteractorType.Anchor)
        {
            // Move LeftParent to RightParent
            reusableData.LeftObject.Move(reusableData.LeftParent, reusableData.RightParent);
        }

        if (leftInteraction.interactorType != InteractorType.Anchor)
        {
            // Move RightParent to LeftParent
            reusableData.RightObject.Move(reusableData.RightParent, reusableData.LeftParent);
        }
        leftInteraction.Interactable(true);
        
    }
}
