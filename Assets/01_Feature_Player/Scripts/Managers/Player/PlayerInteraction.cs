using System;
using UnityEngine;

public class PlayerInteraction : InteractionSystem
{
    public event Action OnEnemyInteract;
    public event Action OnPlatformInteract;
    public event Action OnToolInteract;
    
    public float boostSpeedEndTransportation = 0.2f;
    
    public override void Interact(InteractionSystem otherSystem)
    {
        base.Interact(otherSystem);
        
        if (otherSystem.interactorType == InteractorType.Enemy)
        {
            // In case the Player is coming into the enemy // LOOSE GAME
            OnEnemyInteract?.Invoke();
            return;
        }

        if (otherSystem.interactorType == InteractorType.Anchor)
        {
            // In case a Player is coming into an Anchor
            PlayerReusableStateData reusableData = GetComponent<PlayerAbilityManager>().ReusableData;
            if (reusableData.LeftActivation)
            {
                reusableData.RightInput = true;
            }
            else
            {
                reusableData.LeftInput = true;
            }
            // Recall Tool 
            AbilityStateMachine abilityStateMachine = GetComponent<PlayerAbilityManager>().AbilityStateMachine;
            abilityStateMachine.ChangeState(abilityStateMachine.RecallState);
            MovementStateMachine stateMachine = GetComponent<PlayerMovementManager>().StateMachine;
            if (otherSystem._onWall)
            {
                Debug.Log("Interaction Wall");
                GetComponent<WallCheck>().SetCanWallRun(true);
                //stateMachine.ChangeState(stateMachine.WallRunState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.JumpState);
            }
            return;
        }

        if (otherSystem.interactorType == InteractorType.Tool)
        {
            // In case the Player trigger into the tool // DESTROY TOOL
            OnToolInteract?.Invoke();
            GetComponent<PlayerMetricsManager>().AddExternForce(boostSpeedEndTransportation);
            PlayerReusableStateData reusableData = GetComponent<PlayerAbilityManager>().ReusableData;

            if (reusableData.LeftActivation)
            {
                if (reusableData.LeftParent != null)
                {
                    reusableData.LeftParent = null;
                }
            }
            else
            {
                if (reusableData.RightParent != null)
                {
                    reusableData.RightParent = null;
                }
            }
            return;
        }
    }
}
