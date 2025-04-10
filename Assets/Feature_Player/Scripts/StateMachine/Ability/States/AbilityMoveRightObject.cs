using System;
using UnityEngine;

public class AbilityMoveRightObject : AbilityTransportState
{
    public event Action OnLeftActivation;
    
    public AbilityMoveRightObject(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        OnLeftActivation?.Invoke();

        rightInteraction = reusableData.RightObject.GetComponent<InteractionSystem>();
        
        reusableData.OnTransportation = false;
    }

    public override void FixedTick()
    {
        base.FixedTick();
        
        MoveRightObject();
    }
    
    public override void Exit()
    {
        base.Exit();
        
        MoveRightObjectToLaunch();
    }

    private void MoveRightObject()
    {
        if (reusableData.LeftParent != null)
        {
            rightInteraction.Interactable(true);
            // Move RightParent to LeftParent
            reusableData.RightObject.Move(reusableData.RightParent, reusableData.LeftParent);
        }
        else
        {
            // Move RightParent to cover Player
            reusableData.RightObject.SetLaunch(false);
            reusableData.RightObject.Move(reusableData.RightParent, _stateMachine.AbilityManager.LeftCloseLauncherTransform.gameObject);
        }
    }
}
