using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityManager : PlayerManager
{
    public PlayerAbilityStateMachine playerAbilityStateMachine { get; private set; }
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

        playerAbilityStateMachine = new PlayerAbilityStateMachine(this);
        playerAbilityStateMachine.ChangeState(playerAbilityStateMachine.ReadyAbilityState);
    }

    private void Update()
    {
        playerAbilityStateMachine?.HandleInput();

        playerAbilityStateMachine?.Tick();

        if(playerAbilityStateMachine != null)
        {
            currentState = playerAbilityStateMachine.currentState;
        }
    }

    private void FixedUpdate()
    {
        playerAbilityStateMachine?.FixedTick();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(playerAbilityStateMachine.currentState == playerAbilityStateMachine.TransportationState)
        {
            if(collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Platform"))
            {
                playerAbilityStateMachine.ChangeState(playerAbilityStateMachine.ReloadAbilityState);
                if(ReusableData.ProjectileRef != null)
                {
                    Destroy(ReusableData.ProjectileRef.gameObject);
                }
            }
        }
    }

    public void ChangeMovementToJump()
    {
        
    }

    public void UseAbility()
    {
        ProjectileManager projectile = Instantiate(Metrics.CurrentPlayerSO.AbilityData.ShootData.ProjectilePrefab, LauncherTransform.position, Camera.transform.rotation);
        projectile.Init(this, LauncherTransform.position, Camera.transform.forward);
        ReusableData.ProjectileRef = projectile;
        playerAbilityStateMachine.ChangeState(playerAbilityStateMachine.StandbyState);
    }

    public void CancelCallBack()
    {
        playerAbilityStateMachine.ChangeState(playerAbilityStateMachine.ReloadAbilityState);
    }
}
