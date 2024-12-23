using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityManager : PlayerManager, I_Initializer
{
    public PlayerAbilityStateMachine playerRightAbilityStateMachine { get; private set; }
    public PlayerAbilityStateMachine playerLeftAbilityStateMachine { get; private set; }
    private PlayerMovementManager playerMovementManager;

    [field: SerializeField] public Transform LauncherTransform { get; private set; }

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

        // On Initialise les 2 States Machines pour chacun des objets
        playerRightAbilityStateMachine = new PlayerAbilityStateMachine(this, true);
        //playerRightAbilityStateMachine.ChangeState(playerRightAbilityStateMachine.ReadyState);
        playerLeftAbilityStateMachine = new PlayerAbilityStateMachine(this, false);
        //playerLeftAbilityStateMachine.ChangeState(playerLeftAbilityStateMachine.ReadyState);
    }

    private void Update()
    {
        playerRightAbilityStateMachine?.HandleInput();

        playerRightAbilityStateMachine?.Tick();

        if(playerRightAbilityStateMachine != null)
        {
            currentState = playerRightAbilityStateMachine.currentState;
        }

        playerLeftAbilityStateMachine?.HandleInput();

        playerLeftAbilityStateMachine?.Tick();

        if (playerLeftAbilityStateMachine != null)
        {
            currentState = playerLeftAbilityStateMachine.currentState;
        }
    }

    private void FixedUpdate()
    {
        playerRightAbilityStateMachine?.FixedTick();

        playerLeftAbilityStateMachine?.FixedTick();
    }

    public void UseAbility(bool isRight)
    {
        
    }

    public void CancelCallBack()
    {
        //playerRightAbilityStateMachine.ChangeState(playerRightAbilityStateMachine.ReloadState);
    }
}
