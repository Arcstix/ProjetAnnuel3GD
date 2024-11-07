using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : PlayerManager
{
    [field : Header("Collider")]
    [field : SerializeField] public CapsuleColliderUtility CapsuleColliderUtility { get; private set; }

    private PlayerMovementStateMachine playerMovementStateMachine;
    private PlayerAbilityManager playerAbilityManager;

    public PlayerReusableStateData ReusableData { get; set; }
    public PlayerMovementStateMachine PlayerMovementStateMachine { get => playerMovementStateMachine; }

    protected override void Awake()
    {
        base.Awake();
        playerAbilityManager = GetComponent<PlayerAbilityManager>();
    }

    public void Init()
    {
        if (ReusableData == null)
        {
            ReusableData = new PlayerReusableStateData();
            playerAbilityManager.ReusableData = ReusableData;
        }

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

        if (CameraManager != null)
        {
            SetThirdPersonMode();
        }
    }

    
    private void OnEnable()
    {
        if (CameraManager != null)
        {
            CameraManager.FirstCameraViewEvent += SetFirstPersonMode;
            CameraManager.ThirdCameraViewEvent += SetThirdPersonMode;
        }
    }

    private void OnDisable()
    {
        if (CameraManager != null)
        {
            CameraManager.FirstCameraViewEvent -= SetFirstPersonMode;
            CameraManager.ThirdCameraViewEvent -= SetThirdPersonMode;
        }
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
        Gizmos.DrawLine(transform.position + CapsuleColliderUtility.CapsuleColliderData.Collider.bounds.center, transform.position + (CapsuleColliderUtility.CapsuleColliderData.Collider.bounds.center - new Vector3(0f, CapsuleColliderUtility.SlopeData.DistanceGroundCheck, 0f)));
    }
}
