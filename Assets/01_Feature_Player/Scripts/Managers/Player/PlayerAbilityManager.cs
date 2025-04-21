using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerAbilityManager : PlayerManager, I_Initializer
{
    [SerializeField] private Transform rightLauncherTransform;
    [SerializeField] private Transform leftLauncherTransform;
    
    [SerializeField] private Transform rightProjectileLauncherTransform;
    [SerializeField] private Transform leftProjectileLauncherTransform;
    
    [SerializeField] private Transform rightCloseLauncherTransform;
    [SerializeField] private Transform leftCloseLauncherTransform;

    [SerializeField] private Transform aimTransform;
    
    public bool aimBotMode = false;
    public bool thirdPersonMode = false;
    public bool useStamina = false;
    
    
    private AbilityStateMachine abilityStateMachine;
    private PlayerReusableStateData reusableData;
    private PlayerTargetSystem _targetSystem;
    
    public event Action OnAbilityStarted;

    public PlayerReusableStateData ReusableData { get => reusableData; set => reusableData = value; }

    public AbilityStateMachine AbilityStateMachine { get => abilityStateMachine; private set => abilityStateMachine = value; }
    public PlayerTargetSystem TargetSystem => _targetSystem;
    public Transform RightLauncherTransform => rightLauncherTransform;
    public Transform LeftLauncherTransform => leftLauncherTransform;
    public Transform RightCloseLauncherTransform => rightCloseLauncherTransform;
    public Transform LeftCloseLauncherTransform => leftCloseLauncherTransform;
    public Transform RightProjectileLauncherTransform => rightProjectileLauncherTransform;
    public Transform LeftProjectileLauncherTransform => leftProjectileLauncherTransform;
    public Transform AimTransform => aimTransform;

    public void Init(PlayerReusableStateData reusableStateData)
    {
        reusableData = reusableStateData;
        _targetSystem = GetComponent<PlayerTargetSystem>();
        abilityStateMachine = new AbilityStateMachine(this);
        abilityStateMachine.ChangeState(abilityStateMachine.IdleState);
        OnAbilityStarted?.Invoke();
    }

    private void Update()
    {
        abilityStateMachine?.HandleInput();

        abilityStateMachine?.Tick();
    }

    private void FixedUpdate()
    {
        abilityStateMachine?.FixedTick();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        
        Gizmos.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.right * 5f);
        
        Gizmos.color = Color.blue;
        
        Gizmos.DrawRay(transform.position + new Vector3(0, 0.5f, 0), -transform.right * 5f);
    }
}
