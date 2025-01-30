using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : PlayerManager, I_Initializer
{
    [field : Header("Collider")]
    [field : SerializeField] public CapsuleColliderUtility CapsuleColliderUtility { get; private set; }

    private PlayerMovementStateMachine movementStateMachine;
    private PlayerReusableStateData reusableData;

    public event Action OnMovementStarted;
    
    public PlayerReusableStateData ReusableData { get => reusableData; set => reusableData = value; }

    public PlayerMovementStateMachine MovementStateMachine { get => movementStateMachine; }

    public void Init(PlayerReusableStateData reusableStateData)
    {
        reusableData = reusableStateData;

        movementStateMachine = new PlayerMovementStateMachine(this);
        movementStateMachine.ChangeState(movementStateMachine.IdleState);
        OnMovementStarted?.Invoke();
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
        movementStateMachine.ChangeState(movementStateMachine.IdleState);
    }

    private void SetThirdPersonMode()
    {
        ReusableData.CanMove = true;
    }

    private void Update()
    {
        movementStateMachine?.HandleInput();

        movementStateMachine?.Tick();
    }

    private void FixedUpdate()
    {
        movementStateMachine?.FixedTick();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(0, CapsuleColliderUtility.CapsuleColliderData.Collider.center.y, 0), transform.position + new Vector3(0, CapsuleColliderUtility.CapsuleColliderData.Collider.center.y - CapsuleColliderUtility.SlopeData.DistanceGroundCheck, 0));
    }
}
