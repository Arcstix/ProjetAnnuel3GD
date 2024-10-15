using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityManager : PlayerManager
{
    private PlayerAbilityStateMachine playerAbilityStateMachine;
    private PlayerMovementManager playerMovementManager;

    [field: SerializeField] public Transform LauncherTransform { get; private set; }

    public PlayerReusableStateData ReusableData { get; set; }

    protected override void Awake()
    {
        base.Awake();
        playerMovementManager = GetComponent<PlayerMovementManager>();

        if(ReusableData == null)
        {
            ReusableData = new PlayerReusableStateData();
            playerMovementManager.ReusableData = ReusableData;
        }

        playerAbilityStateMachine = new PlayerAbilityStateMachine(this);
    }

    private void Start()
    {
        playerAbilityStateMachine.ChangeState(playerAbilityStateMachine.ReadyAbilityState);
    }

    private void Update()
    {
        playerAbilityStateMachine.HandleInput();

        playerAbilityStateMachine.Tick();
    }

    private void FixedUpdate()
    {
        playerAbilityStateMachine.FixedTick();
    }

    public void UseAbility()
    {
        Debug.Log("Projectile instancier");
        ProjectileManager projectile = Instantiate(PlayerSO.AbilityData.ShootData.ProjectilePrefab, LauncherTransform.position, Quaternion.identity);
        projectile.Init(this, LauncherTransform.forward);
        ReusableData.ProjectileRef = projectile;
        playerAbilityStateMachine.ChangeState(playerAbilityStateMachine.StandbyState);
    }

    public void CancelCallBack()
    {
        playerAbilityStateMachine.ChangeState(playerAbilityStateMachine.ReloadAbilityState);
    }
}
