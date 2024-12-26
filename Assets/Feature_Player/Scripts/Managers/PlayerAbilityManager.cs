using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerAbilityManager : PlayerManager, I_Initializer
{
    [SerializeField] private Transform rightLauncherTransform;
    [SerializeField] private Transform leftLauncherTransform;
    
    private PlayerMovementManager playerMovementManager;
    private AbilityStateMachine abilityStateMachine;

    public AbilityStateMachine AbilityStateMachine { get => abilityStateMachine; private set => abilityStateMachine = value; }
    public PlayerReusableStateData ReusableData { get; set; }
    public Transform RightLauncherTransform { get => rightLauncherTransform; set => rightLauncherTransform = value; }
    
    public Transform LeftLauncherTransform { get => leftLauncherTransform; set => leftLauncherTransform = value; }

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
}
