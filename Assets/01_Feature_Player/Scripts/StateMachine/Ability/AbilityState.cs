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

    protected InputAction rightShootRecall;
    protected InputAction leftShootRecall;
    protected InputAction rightShootThrow;
    protected InputAction leftShootThrow;
    protected InputAction rightThrow;
    protected InputAction leftThrow;
    protected InputAction leftAttraction;
    protected InputAction rightAttraction;
    protected InputAction recall;
    
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

        rightShootRecall = input.actions["ShootRecallRight"];
        leftShootRecall = input.actions["ShootRecallLeft"];
        rightShootThrow = input.actions["ShootThrowRight"];
        leftShootThrow = input.actions["ShootThrowLeft"];
        rightThrow = input.actions["ThrowRight"];
        leftThrow = input.actions["ThrowLeft"];
        leftAttraction = input.actions["AttractionLeft"];
        rightAttraction = input.actions["AttractionRight"];
        recall = input.actions["Recall"];
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

    #region Change States methods
    
    #region AimState
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
    #endregion
    
    #region ShootState / RecallState
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
    
    #endregion
    
    #region Shoot / Throw State
    protected void HandleRightShootThrow()
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
            if (targetSystem.rightTargetLaunch != null)
            {
                if (targetSystem.rightTargetLaunch.GetCurrentType() == InteractorType.Projectile)
                {
                    reusableData.RightThrow = true;
                    _stateMachine.ChangeState(_stateMachine.ThrowState);
                }
            }
        }
    }
    
    protected void HandleLeftShootThrow()
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
            if (targetSystem.leftTargetLaunch != null)
            {
                if (targetSystem.leftTargetLaunch.GetCurrentType() == InteractorType.Projectile)
                {
                    reusableData.LeftThrow = true;
                    _stateMachine.ChangeState(_stateMachine.ThrowState);
                }
            }
        }
    }
    
    #endregion
    
    #region AttractionState

    protected void HandleRightAttraction()
    {
        if (reusableData.LeftObject == null && reusableData.RightObject == null)
        {
            return;
        }
        else
        {
            // Case : Attraction de RightObject vers LeftObject
            if (reusableData.RightObject != null && reusableData.LeftObject != null)
            {
                _stateMachine.ChangeState(_stateMachine.MoveLeftObject);
                return;
            }
            
            // Si on arrive ici c'est que l'un des 2 est null
            
            // Case : RightObject == null && LeftObject != null --> On attire LeftObject vers soit
            if (reusableData.RightObject == null)
            {
                _stateMachine.ChangeState(_stateMachine.MoveLeftObject);
                return;
            }
            
            // Case : RightObject != null && LeftObject == null --> Attraction du Player vers la cible
            if (reusableData.LeftObject == null)
            {
                if (reusableData.RightParent.GetComponent<InteractiveTarget>().GetCurrentType() !=
                    InteractorType.Projectile)
                {
                    _stateMachine.ChangeState(_stateMachine.MovePlayer);
                }
            }
        }
    }

    protected void HandleLeftAttraction()
    {
        if (reusableData.LeftObject == null && reusableData.RightObject == null)
        {
            return;
        }
        else
        {
            // Case : Attraction de LeftObject vers RightObject
            if (reusableData.RightObject != null && reusableData.LeftObject != null)
            {
                _stateMachine.ChangeState(_stateMachine.MoveRightObject);
                return;
            }
            
            // Si on arrive ici c'est que l'un des 2 est null
            
            // Case : RightObject != null && LeftObject == null --> On attire RightObject vers soit
            if (reusableData.LeftObject == null)
            {
                _stateMachine.ChangeState(_stateMachine.MoveRightObject);
            }
            
            // Case : RightObject == null && LeftObject != null --> Attraction du Player vers la cible
            if (reusableData.RightObject == null)
            {
                if (reusableData.LeftParent.GetComponent<InteractiveTarget>().GetCurrentType() !=
                    InteractorType.Projectile)
                {
                    _stateMachine.ChangeState(_stateMachine.MovePlayer);
                }
                return;
            }
        }
    }
    
    #endregion

    protected void HandleRecall()
    {
        reusableData.RightInput = true;
        reusableData.LeftInput = true;
        
        _stateMachine.ChangeState(_stateMachine.RecallState);
    }
    
    #endregion
    
    #region Utility Methods
    
    public void CheckToolPresence()
    {
        // Important car on ne peut pas rester dans cette state si ce n'est pas vérifié.
        if (reusableData.RightObject == null && reusableData.LeftObject == null)
        {
            reusableData.LeftParent = null;
            reusableData.RightParent = null;
            
            leftLauncher.GetComponent<MeshRenderer>().enabled = true;
            rightLauncher.GetComponent<MeshRenderer>().enabled = true;
            
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
    
    #endregion
}
