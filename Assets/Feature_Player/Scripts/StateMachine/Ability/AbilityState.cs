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
    protected PlayerCameraManager cameraManager;
    protected PlayerReusableStateData reusableData;
    protected Rigidbody rigidbody;
    
    protected Transform rightLauncher;
    protected Transform leftLauncher;

    // Sert � la cr�ation de raccourcie.
    // ATTENTION AUX SCRIPTABLES OBJECTS QUI PEUVENT TOTALEMENT CHANGER COMME LE PLAYERSO !
    public AbilityState(AbilityStateMachine abilityStateMachine)
    {
        _stateMachine = abilityStateMachine;
        metricsManager = _stateMachine.AbilityManager.Metrics;
        input = _stateMachine.AbilityManager.Input;
        reusableData = _stateMachine.ReusableStateData;
        rigidbody = _stateMachine.AbilityManager.Rb;

        if(_stateMachine.AbilityManager.CameraManager != null)
        {
            cameraManager = _stateMachine.AbilityManager.CameraManager;
        }
        
        rightLauncher = _stateMachine.AbilityManager.RightLauncherTransform;
        leftLauncher = _stateMachine.AbilityManager.LeftLauncherTransform;
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
        
    }

    public virtual void FixedTick()
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
            _stateMachine.ChangeState(_stateMachine.ShootState);
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
            _stateMachine.ChangeState(_stateMachine.ShootState);
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
