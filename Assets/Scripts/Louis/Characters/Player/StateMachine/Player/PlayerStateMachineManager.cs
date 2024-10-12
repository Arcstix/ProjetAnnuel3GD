using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerStateMachineManager : MonoBehaviour
{
    [field : Header("References")]
    [field : SerializeField] public PlayerSO PlayerSO { get; private set; }

    [field : Header("Collider")]
    [field : SerializeField] public CapsuleColliderUtility CapsuleColliderUtility { get; private set; }

    public Rigidbody Rigidbody { get; private set; }

    public PlayerInput Input { get; private set; }

    public Camera Camera { get; private set; }

    public PlayerCameraManager playerCameraManager { get; private set; }

    private PlayerMovementStateMachine playerMovementStateMachine;

    private PlayerAbilityStateMachine playerAbilityStateMachine;

    public bool CanMove { get; private set; } = true;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();

        Input = GetComponent<PlayerInput>();

        Camera = Camera.main;

        playerMovementStateMachine = new PlayerMovementStateMachine(this);
        playerAbilityStateMachine = new PlayerAbilityStateMachine(this);

        playerCameraManager = GetComponent<PlayerCameraManager>();
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
        playerCameraManager.FirstCameraViewEvent += SetFirstPersonMode;
        playerCameraManager.ThirdCameraViewEvent += SetThirdPersonMode;
    }

    private void OnDisable()
    {
        playerCameraManager.FirstCameraViewEvent -= SetFirstPersonMode;
        playerCameraManager.ThirdCameraViewEvent -= SetThirdPersonMode;
    }

    private void SetFirstPersonMode()
    {
        CanMove = false;
        playerMovementStateMachine.ChangeState(playerMovementStateMachine.IdleState);
    }

    private void SetThirdPersonMode()
    {
        CanMove = true;
    }

    private void Update()
    {
        playerMovementStateMachine.HandleInput();

        playerMovementStateMachine.Tick();

        playerAbilityStateMachine.HandleInput();

        playerAbilityStateMachine.Tick();
    }

    private void FixedUpdate()
    {
        playerMovementStateMachine.FixedTick();

        playerAbilityStateMachine.FixedTick();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + CapsuleColliderUtility.CapsuleColliderData.Collider.center, transform.position + (CapsuleColliderUtility.CapsuleColliderData.Collider.center - new Vector3(0f, CapsuleColliderUtility.SlopeData.DistanceGroundCheck, 0f)));
    }
}
