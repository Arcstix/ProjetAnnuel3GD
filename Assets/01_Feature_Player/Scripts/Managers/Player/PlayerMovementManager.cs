using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PlayerMovementManager : PlayerManager, I_Initializer
{
    public bool hasExternalForces = false;
    
    [field : Header("Collider")]
    [field : SerializeField] public CapsuleColliderUtility CapsuleUtility { get; private set; }
    
    public float sphereGroundCheckRadius = 0.2f;
    public float sphereDistanceGroundCheck = 0.5f;

    private MovementStateMachine stateMachine;
    private PlayerReusableStateData reusableData;
    private GroundCheck groundChecker;

    public event Action OnMovementStarted;
    
    public PlayerReusableStateData ReusableData { get => reusableData; set => reusableData = value; }

    public MovementStateMachine StateMachine => stateMachine;
    
    public GroundCheck GroundChecker => groundChecker;
    
    public void Init(PlayerReusableStateData reusableStateData)
    {
        reusableData = reusableStateData;

        stateMachine = new MovementStateMachine(this);
        stateMachine.ChangeState(stateMachine.IdleState);
        OnMovementStarted?.Invoke();
    }

    private void OnValidate()
    {
        CapsuleUtility.Initialize(gameObject);
        CapsuleUtility.CalculateCapsuleColliderDimension();
    }

    private void Start()
    {        
        CapsuleUtility.Initialize(gameObject);
        CapsuleUtility.CalculateCapsuleColliderDimension();
    }

    private void SetFirstPersonMode()
    {
        ReusableData.CanMove = false;
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void SetThirdPersonMode()
    {
        ReusableData.CanMove = true;
    }

    private void Update()
    {
        stateMachine?.HandleInput();

        stateMachine?.Tick();
    }

    private void FixedUpdate()
    {
        stateMachine?.FixedTick();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Collision Wall Enter");
        }
        stateMachine?.OnCollisionEnter(other);
    }

    private void OnCollisionExit(Collision other)
    {
        stateMachine?.OnCollisionExit(other);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(0, CapsuleUtility.CapsuleColliderData.Collider.center.y, 0), transform.position + new Vector3(0, CapsuleUtility.CapsuleColliderData.Collider.center.y - CapsuleUtility.SlopeData.DistanceGroundCheck, 0));

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(
            new Vector3(transform.position.x, CapsuleUtility.CapsuleColliderData.Collider.bounds.center.y - sphereDistanceGroundCheck,
                transform.position.z), sphereGroundCheckRadius);
        
    }
}
