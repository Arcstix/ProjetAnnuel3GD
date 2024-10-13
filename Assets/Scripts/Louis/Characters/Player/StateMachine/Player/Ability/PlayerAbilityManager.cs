using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityManager : PlayerManager
{
    private PlayerAbilityStateMachine playerAbilityStateMachine;

    [field: SerializeField] public Transform LauncherTransform { get; private set; }

    protected override void Awake()
    {
        base.Awake();

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
