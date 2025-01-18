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
    
    private AbilityStateMachine abilityStateMachine;
    private PlayerReusableStateData reusableData;

    public PlayerReusableStateData ReusableData { get => reusableData; set => reusableData = value; }

    public AbilityStateMachine AbilityStateMachine { get => abilityStateMachine; private set => abilityStateMachine = value; }
    public Transform RightLauncherTransform { get => rightLauncherTransform; set => rightLauncherTransform = value; }
    
    public Transform LeftLauncherTransform { get => leftLauncherTransform; set => leftLauncherTransform = value; }

    public IState currentState;

    public void Init(PlayerReusableStateData reusableStateData)
    {
        reusableData = reusableStateData;
        
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
