using System;
using UnityEngine;

public class AbilityMoveLeftObject : AbilityTransportState
{
    public event Action OnRightActivation;
    
    public AbilityMoveLeftObject(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        OnRightActivation?.Invoke();

        leftInteraction = reusableData.LeftParent.GetComponent<InteractionSystem>();
        
        reusableData.OnTransportation = false;
    }

    public override void FixedTick()
    {
        base.FixedTick();

        if (leftInteraction.interactorType != InteractorType.Anchor)
        {
            MoveLeftObject();
        }
    }
    
    public override void Exit()
    {
        base.Exit();
        
        MoveLeftObjectToLaunch();
    }

    private void MoveLeftObject()
    {
        if (reusableData.RightParent != null)
        {
            leftInteraction.Interactable(true);
                
            // Move LeftParent to RightParent
            reusableData.LeftObject.Move(reusableData.LeftParent, reusableData.RightParent);
        }
        else
        {
            // Move LeftParent to cover Player
            reusableData.LeftObject.SetLaunch(false);
            reusableData.LeftObject.Move(reusableData.LeftParent, _stateMachine.AbilityManager.RightCloseLauncherTransform.gameObject);
        }
    }
}
