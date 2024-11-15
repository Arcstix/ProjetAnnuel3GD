using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityManager : PlayerManager
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

        AddInputSwitchAttraction();
        // On Initialise les 2 States Machines pour chacun des objets
        playerRightAbilityStateMachine = new PlayerAbilityStateMachine(this, true);
        playerRightAbilityStateMachine.ChangeState(playerRightAbilityStateMachine.ReadyState);
        playerLeftAbilityStateMachine = new PlayerAbilityStateMachine(this, false);
        playerLeftAbilityStateMachine.ChangeState(playerLeftAbilityStateMachine.ReadyState);
    }

    private void OnDisable()
    {
        RemoveInputSwitchAttraction();
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

    private void OnCollisionEnter(Collision collision)
    {
        if(playerRightAbilityStateMachine.currentState == playerRightAbilityStateMachine.TransportationState)
        {
            if(collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Platform"))
            {
                playerRightAbilityStateMachine.ChangeState(playerRightAbilityStateMachine.ReloadState);
            }
        }

        if (playerLeftAbilityStateMachine.currentState == playerLeftAbilityStateMachine.TransportationState)
        {
            if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Platform"))
            {
                playerLeftAbilityStateMachine.ChangeState(playerLeftAbilityStateMachine.ReloadState);
            }
        }
    }

    public void UseAbility(bool isRight)
    {
        ProjectileManager projectile = Instantiate(Metrics.CurrentPlayerSO.AbilityData.ShootData.ProjectilePrefab, LauncherTransform.position, Camera.transform.rotation);
        projectile.Init(this, LauncherTransform.position, Camera.transform.forward);

        if (isRight)
        {
            ReusableData.RightProjectileRef = projectile;
            playerRightAbilityStateMachine.ChangeState(playerRightAbilityStateMachine.StandbyState);
        }
        else
        {
            ReusableData.LeftProjectileRef = projectile;
            playerLeftAbilityStateMachine.ChangeState(playerLeftAbilityStateMachine.StandbyState);
        }
    }

    /// <summary>
    /// On s'abonne à l'input qui sert de changement d'attraction
    /// </summary>
    private void AddInputSwitchAttraction()
    {
        Input.PlayerActions.SwitchAttraction.started += SwitchMode;
    }

    /// <summary>
    /// On se désabonne à l'input qui sert de changement d'attraction
    /// </summary>
    private void RemoveInputSwitchAttraction()
    {
        Input.PlayerActions.SwitchAttraction.started -= SwitchMode;
    }

    private void SwitchMode(InputAction.CallbackContext context)
    {
        ReusableData.TransportationMode = !ReusableData.TransportationMode;
    }

    public void CancelCallBack()
    {
        playerRightAbilityStateMachine.ChangeState(playerRightAbilityStateMachine.ReloadState);
    }
}
