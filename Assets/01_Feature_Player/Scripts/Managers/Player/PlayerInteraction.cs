using System;
using UnityEngine;

public class PlayerInteraction : InteractionSystem
{
    public event Action OnEnemyInteract;
    public event Action OnPlatformInteract;
    public event Action OnDestructibleInteract;
    public event Action OnToolInteract;

    private PlayerAbilityManager abilityManager;
    private PlayerMovementManager movementManager;
    private PlayerMetricsManager metricsManager;
    
    public float boostSpeedEndTransportation = 0.2f;

    private void Awake()
    {
        abilityManager = GetComponent<PlayerAbilityManager>();
        movementManager = GetComponent<PlayerMovementManager>();
        metricsManager = GetComponent<PlayerMetricsManager>();
    }

    public override void Interact(InteractionSystem otherSystem)
    {
        base.Interact(otherSystem);
        
        if (otherSystem.interactorType == InteractorType.Enemy)
        {
            // In case the Player is coming into the enemy // LOOSE GAME
            OnEnemyInteract?.Invoke();
            abilityManager.AbilityStateMachine?.ChangeState(abilityManager.AbilityStateMachine.RecallState);
            metricsManager.RecoverFullCharge();
            movementManager.StateMachine?.ChangeState(movementManager.StateMachine.JumpState);
            return;
        }

        if (otherSystem.interactorType == InteractorType.Anchor)
        {
            // In case a Player is coming into an Anchor
            PlayerReusableStateData reusableData = abilityManager.ReusableData;
            if (reusableData.LeftActivation)
            {
                reusableData.RightInput = true;
            }
            else
            {
                reusableData.LeftInput = true;
            }
            // Recall Tool 
            abilityManager.AbilityStateMachine?.ChangeState(abilityManager.AbilityStateMachine?.RecallState);
            if (otherSystem._onWall)
            {
                Debug.Log("Interaction Wall");
                GetComponent<WallCheck>().SetCanWallRun(true);
                //stateMachine.ChangeState(stateMachine.WallRunState);
            }
            else
            {
                movementManager.StateMachine?.ChangeState(movementManager.StateMachine?.JumpState);
            }
            return;
        }

        if (otherSystem.interactorType == InteractorType.Tool)
        {
            // In case the Player trigger into the tool // DESTROY TOOL
            OnToolInteract?.Invoke();
            metricsManager.AddExternForce(boostSpeedEndTransportation);
            PlayerReusableStateData reusableData = abilityManager.ReusableData;
            abilityManager.AbilityStateMachine?.ChangeState(abilityManager.AbilityStateMachine?.RecallState);
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

        if (otherSystem.interactorType == InteractorType.InstantDestructible)
        {
            OnDestructibleInteract?.Invoke();
            abilityManager.AbilityStateMachine?.ChangeState(abilityManager.AbilityStateMachine?.RecallState);
            metricsManager.RecoverFullCharge();
        }
    }
}
