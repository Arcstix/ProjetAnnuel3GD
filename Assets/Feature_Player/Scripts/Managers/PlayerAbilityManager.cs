using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerAbilityManager : PlayerManager, I_Initializer
{
    [SerializeField] public Transform launcherTransform;
    
    private PlayerMovementManager playerMovementManager;
    private AbilityStateMachine abilityStateMachine;

    public AbilityStateMachine AbilityStateMachine { get => abilityStateMachine; private set => abilityStateMachine = value; }
    public PlayerReusableStateData ReusableData { get; set; }

    public IState currentState;
    
    protected override void Awake()
    {
        base.Awake();
        playerMovementManager = GetComponent<PlayerMovementManager>();
    }

    public void Init()
    {
        if (ReusableData == null)
        {
            ReusableData = new PlayerReusableStateData();
            playerMovementManager.ReusableData = ReusableData;
        }
        
        abilityStateMachine = new AbilityStateMachine(this);
        abilityStateMachine.ChangeState(abilityStateMachine.IdleState);
    }

    private void Update()
    {
        abilityStateMachine?.HandleInput();

        abilityStateMachine?.Tick();

        if(abilityStateMachine != null)
        {
            currentState = abilityStateMachine.currentState;
        }
    }

    private void FixedUpdate()
    {
        abilityStateMachine?.FixedTick();
    }

    public void UseAbility(bool isRight)
    {
        
    }

    public void CancelCallBack()
    {
        //playerRightAbilityStateMachine.ChangeState(playerRightAbilityStateMachine.ReloadState);
    }
}
