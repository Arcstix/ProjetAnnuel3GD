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

    private void OnCollisionEnter(Collision collision)
    {
        if(playerAbilityStateMachine.currentState == playerAbilityStateMachine.TransportationState)
        {
            if(collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Platform"))
            {
                playerAbilityStateMachine.ChangeState(playerAbilityStateMachine.ReloadAbilityState);
                if(ReusableData.ProjectileRef != null)
                {
                    Debug.Log("Projectile détruit");
                    Destroy(ReusableData.ProjectileRef.gameObject);
                }
            }
        }
    }

    public void UseAbility()
    {
        ProjectileManager projectile = Instantiate(PlayerSO.AbilityData.ShootData.ProjectilePrefab, LauncherTransform.position, Camera.transform.rotation);
        projectile.Init(this, LauncherTransform.position, Camera.transform.forward);
        ReusableData.ProjectileRef = projectile;
        playerAbilityStateMachine.ChangeState(playerAbilityStateMachine.StandbyState);
    }

    public void CancelCallBack()
    {
        playerAbilityStateMachine.ChangeState(playerAbilityStateMachine.ReloadAbilityState);
    }
}
