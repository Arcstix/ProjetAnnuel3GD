using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerStateMachineManager : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    public PlayerInput Input { get; private set; }

    public Camera Camera { get; private set; }

    private PlayerMovementStateMachine playerMovementStateMachine;

    private PlayerAbilityStateMachine playerAbilityStateMachine;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();

        Input = GetComponent<PlayerInput>();

        Camera = Camera.main;

        playerMovementStateMachine = new PlayerMovementStateMachine(this);
    }

    private void Start()
    {
        playerMovementStateMachine.ChangeState(playerMovementStateMachine.IdleState);
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
}
