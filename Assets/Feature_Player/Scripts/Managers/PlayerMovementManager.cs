using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : PlayerManager, I_Initializer
{
    [field : Header("Collider")]
    [field : SerializeField] public CapsuleColliderUtility CapsuleColliderUtility { get; private set; }

    private PlayerMovementStateMachine playerMovementStateMachine;
    private PlayerReusableStateData reusableData;

    public PlayerReusableStateData ReusableData { get => reusableData; set => reusableData = value; }

    public PlayerMovementStateMachine PlayerMovementStateMachine { get => playerMovementStateMachine; }

    public void Init(PlayerReusableStateData reusableStateData)
    {
        reusableData = reusableStateData;

        playerMovementStateMachine = new PlayerMovementStateMachine(this);
        playerMovementStateMachine.ChangeState(playerMovementStateMachine.IdleState);
    }

    private void OnValidate()
    {
        CapsuleColliderUtility.Initialize(gameObject);
        CapsuleColliderUtility.CalculateCapsuleColliderDimension();
    }

    private void Start()
    {        
        CapsuleColliderUtility.Initialize(gameObject);
        CapsuleColliderUtility.CalculateCapsuleColliderDimension();
    }

    private void SetFirstPersonMode()
    {
        ReusableData.CanMove = false;
        playerMovementStateMachine.ChangeState(playerMovementStateMachine.IdleState);
    }

    private void SetThirdPersonMode()
    {
        ReusableData.CanMove = true;
    }

    private void Update()
    {
        playerMovementStateMachine?.HandleInput();

        playerMovementStateMachine?.Tick();
    }

    private void FixedUpdate()
    {
        playerMovementStateMachine?.FixedTick();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(0, CapsuleColliderUtility.CapsuleColliderData.Collider.center.y, 0), transform.position + new Vector3(0, CapsuleColliderUtility.CapsuleColliderData.Collider.center.y - CapsuleColliderUtility.SlopeData.DistanceGroundCheck, 0));
    }
}
