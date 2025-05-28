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

        rightInteraction = reusableData.RightParent.GetComponent<InteractionSystem>();
        
        reusableData.OnTransportation = false;
    }

    public override void FixedTick()
    {
        base.FixedTick();

        if (rightInteraction.interactorType != InteractorType.Anchor && leftInteraction.interactorType != InteractorType.MobilePlatform)
        {
            MoveRightObject();
        }
        
        if (rightInteraction.interactorType == InteractorType.MobilePlatform)
        {
            rightInteraction.GetComponent<MobilePlatformInteraction>().MovePlatform();
        }
    }
    
    public override void Exit()
    {
        base.Exit();
        
        MoveRightObjectToLaunch();
    }

    private void MoveRightObject()
    {
        if (reusableData.LeftObject)
        {
            rightInteraction.Interactable(true);
            if (reusableData.LeftParent != null)
            {
                // Move RightParent to LeftParent
                reusableData.RightObject.Move(reusableData.RightParent, reusableData.LeftParent);
            }
            else
            {
                // Move RightParent to LeftObject
                reusableData.RightObject.Move(reusableData.RightParent, reusableData.LeftObject.gameObject);
            }
        }
        else
        {
            // Move RightParent to cover Player
            reusableData.RightObject.SetLaunch(false);
            reusableData.RightObject.Move(reusableData.RightParent, _stateMachine.AbilityManager.LeftCloseLauncherTransform.gameObject);
        }
    }
}
