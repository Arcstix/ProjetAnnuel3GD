using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovementManager : PlayerManager
{
    [field : Header("Collider")]
    [field : SerializeField] public CapsuleColliderUtility CapsuleColliderUtility { get; private set; }

    private PlayerMovementStateMachine playerMovementStateMachine;
    private PlayerAbilityManager playerAbilityManager;

    public PlayerReusableStateData ReusableData { get; set; }

    protected override void Awake()
    {
        base.Awake();
        playerAbilityManager = GetComponent<PlayerAbilityManager>();

        if(ReusableData == null)
        {
            ReusableData = new PlayerReusableStateData();
            playerAbilityManager.ReusableData = ReusableData;
        }

        playerMovementStateMachine = new PlayerMovementStateMachine(this);
    }

    private void OnValidate()
    {
        CapsuleColliderUtility.Initialize(gameObject);
        CapsuleColliderUtility.CalculateCapsuleColliderDimension();
    }

    private void Start()
    {
        playerMovementStateMachine.ChangeState(playerMovementStateMachine.IdleState);

        SetThirdPersonMode();
    }

    
    private void OnEnable()
    {
        CameraManager.FirstCameraViewEvent += SetFirstPersonMode;
        CameraManager.ThirdCameraViewEvent += SetThirdPersonMode;
    }

    private void OnDisable()
    {
        CameraManager.FirstCameraViewEvent -= SetFirstPersonMode;
        CameraManager.ThirdCameraViewEvent -= SetThirdPersonMode;
    }

    protected void SetFirstPersonMode()
    {
        ReusableData.CanMove = false;
        playerMovementStateMachine.ChangeState(playerMovementStateMachine.IdleState);
    }

    protected void SetThirdPersonMode()
    {
        ReusableData.CanMove = true;
    }

    private void Update()
    {
        playerMovementStateMachine.HandleInput();

        playerMovementStateMachine.Tick();
    }

    private void FixedUpdate()
    {
        playerMovementStateMachine.FixedTick();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + CapsuleColliderUtility.CapsuleColliderData.Collider.center, transform.position + (CapsuleColliderUtility.CapsuleColliderData.Collider.center - new Vector3(0f, CapsuleColliderUtility.SlopeData.DistanceGroundCheck, 0f)));
    }
}
