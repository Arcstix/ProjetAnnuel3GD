using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityState : IState
{
    // Mettre les variables "protected" pour que les States puissent y avoir acc�s.
    protected AbilityStateMachine _stateMachine;
    protected PlayerMetricsManager metricsManager;
    protected PlayerInput input;
    protected PlayerReusableStateData reusableData;
    protected Rigidbody rigidbody;
    // target system work without the need of Ability State machine but not the Ability
    protected readonly PlayerTargetSystem targetSystem;
    // Target for throwing projectile
    protected Transform aimTransform;
    
    protected Transform rightLauncher;
    protected Transform leftLauncher;

    protected Transform rightProjectileLauncher;
    protected Transform leftProjectileLauncher;
    
    public event Action ExitTransportation;

    // Sert � la cr�ation de raccourcie.
    // ATTENTION AUX SCRIPTABLES OBJECTS QUI PEUVENT TOTALEMENT CHANGER COMME LE PLAYERSO !
    public AbilityState(AbilityStateMachine abilityStateMachine)
    {
        _stateMachine = abilityStateMachine;
        metricsManager = _stateMachine.AbilityManager.Metrics;
        input = _stateMachine.AbilityManager.Input;
        reusableData = _stateMachine.ReusableStateData;
        rigidbody = _stateMachine.AbilityManager.Rb;
        
        rightLauncher = _stateMachine.AbilityManager.RightLauncherTransform;
        leftLauncher = _stateMachine.AbilityManager.LeftLauncherTransform;
        rightProjectileLauncher = _stateMachine.AbilityManager.RightProjectileLauncherTransform;
        leftProjectileLauncher = _stateMachine.AbilityManager.LeftProjectileLauncherTransform;
        targetSystem = _stateMachine.AbilityManager.TargetSystem;
        aimTransform = _stateMachine.AbilityManager.AimTransform;
    }

    #region State Methods
    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
        
    }

    public virtual void Tick()
    {
        if (!reusableData.OnTransportation)
        {
            ExitTransportation?.Invoke();
        }
    }

    public virtual void FixedTick()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        
    }

    public void OnCollisionExit(Collision collision)
    {
        
    }

    public virtual void HandleInput()
    {
        
    }
    #endregion
    

    #region Ability methods
    
    protected void HandleRightAimState()
    {
        reusableData.RightInput = true;
        _stateMachine.ChangeState(_stateMachine.AimState);
    }
    
    protected void HandleLeftAimState()
    {
        reusableData.LeftInput = true;
        _stateMachine.ChangeState(_stateMachine.AimState);
    }

    protected void HandleRightShootRecall()
    {
        reusableData.RightInput = true;
        if (reusableData.RightObject == null)
        {
            if (!_stateMachine.AbilityManager.aimBotMode || targetSystem.currentTarget != null)
            {
                _stateMachine.ChangeState(_stateMachine.ShootState);
            }
        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.RecallState);
        }
    }

    protected void HandleLeftShootRecall()
    {
        reusableData.LeftInput = true;
        if (reusableData.LeftObject == null)
        {
            if (!_stateMachine.AbilityManager.aimBotMode || targetSystem.currentTarget != null)
            {
                _stateMachine.ChangeState(_stateMachine.ShootState);
            }
        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.RecallState);
        }
    }
    
    protected void HandleAttraction()
    {
        if (reusableData.LeftObject == null && reusableData.RightObject == null)
        {
            return;
        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.TransportState);
        }
    }

    #endregion

}
