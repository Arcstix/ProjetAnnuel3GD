using System;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    // Animation Ability
    private static readonly int AimL = Animator.StringToHash("Aim_L");
    private static readonly int AimR = Animator.StringToHash("Aim_R");
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int RecallL = Animator.StringToHash("Recall_L");
    private static readonly int RecallR = Animator.StringToHash("Recall_R");
    private static readonly int ActivateR = Animator.StringToHash("ActivateR");
    private static readonly int ActivateL = Animator.StringToHash("Activate_L");
    
    // Animation Movement
    private int _speed = Animator.StringToHash("Speed");
    private int _grounded = Animator.StringToHash("Grounded");
    private int _jump = Animator.StringToHash("Jump");
    
    [SerializeField] private Animator animator;
    [SerializeField] private float blendAnimSpeed;
    
    private PlayerAbilityManager abilityManager;
    private PlayerMovementManager movementManager;

    private float _currentSpeed = 0f;
    private float _targetSpeed = 0f;

    private void Update()
    {
        if (_currentSpeed <= _targetSpeed - 0.05f || _currentSpeed > _targetSpeed + 0.05f)
        {
            UpdateSpeed();
            animator.SetFloat(_speed, _currentSpeed);
        }
        else
        {
            _currentSpeed = _targetSpeed;
            animator.SetFloat(_speed, _currentSpeed);
        }
    }

    private void UpdateSpeed()
    {
        _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed, Time.deltaTime * blendAnimSpeed);
    }

    private void OnEnable()
    {
        abilityManager = GetComponent<PlayerAbilityManager>();
        movementManager = GetComponent<PlayerMovementManager>();

        abilityManager.OnAbilityStarted += SubscribeAbilityEvent;
        movementManager.OnMovementStarted += SubscribeMovementEvent;
    }

    private void OnDisable()
    {
        abilityManager.OnAbilityStarted -= SubscribeAbilityEvent;
        movementManager.OnMovementStarted -= SubscribeMovementEvent;
        
        UnSubscribeMovementEvent();
    }

    private void SubscribeAbilityEvent()
    {
        
    }

    private void SubscribeMovementEvent()
    {
        movementManager.StateMachine.IdleState.OnIdle += Idle;
        movementManager.StateMachine.WalkState.OnWalking += Walk;
        movementManager.StateMachine.RunningState.OnRunning += Run;
        movementManager.StateMachine.FallingState.OnFalling += Fall;
        movementManager.StateMachine.JumpState.OnJump += Jump;
        movementManager.StateMachine.LandingState.OnLanding += Land;
        movementManager.StateMachine.OnPlatformState.OnPlatform += Idle;
    }

    private void UnSubscribeMovementEvent()
    {
        movementManager.StateMachine.IdleState.OnIdle -= Idle;
        movementManager.StateMachine.WalkState.OnWalking -= Walk;
        movementManager.StateMachine.RunningState.OnRunning -= Run;
        movementManager.StateMachine.FallingState.OnFalling -= Fall;
        movementManager.StateMachine.JumpState.OnJump -= Jump;
        movementManager.StateMachine.LandingState.OnLanding -= Land;
        movementManager.StateMachine.OnPlatformState.OnPlatform -= Idle;
    }
    
    #region Ability

    private void TriggerAimL()
    {
        animator.SetTrigger(AimL);
    }

    private void TriggerAimR()
    {
        animator.SetTrigger(AimR);
    }

    private void TriggerShoot()
    {
        animator.SetTrigger(Shoot);
    }

    private void TriggerRecallL()
    {
        animator.SetTrigger(RecallL);
    }

    private void TriggerRecallR()
    {
        animator.SetTrigger(RecallR);
    }

    private void TriggerActivateR()
    {
        animator.SetTrigger(ActivateR);
    }

    private void TriggerActivateL()
    {
        animator.SetTrigger(ActivateL);
    }

    #endregion
    
    #region Movement
    private void Idle()
    {
        _targetSpeed = 0;
        animator.SetBool(_grounded, true);
    }

    private void Walk()
    {
        _targetSpeed = 0.5f;
        animator.SetBool(_grounded, true);
    }

    private void Run()
    {
        _targetSpeed = 1;
        animator.SetBool(_grounded, true);
    }

    private void Jump()
    {
        animator.SetBool(_grounded, false);
        animator.SetTrigger(_jump);
    }

    private void Fall()
    {
        animator.ResetTrigger(_jump);
        animator.SetBool(_grounded, false);
    }

    private void Land()
    {
        animator.ResetTrigger(_jump);
        animator.SetFloat(_speed, _currentSpeed);
        animator.SetBool(_grounded, true);
        animator.ResetTrigger(_jump);
    }
    #endregion
}
