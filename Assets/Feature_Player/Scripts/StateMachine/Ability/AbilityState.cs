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

    #region Input method

    #region ThrowRecall

    /// <summary>
    /// On s'abonne � l'input qui sert � Shoot et Recall l'objet de droite
    /// </summary>
    protected virtual void AddInputRightThrowRecall()
    {
        input.PlayerActions.ThrowRecallRight.performed += HandleAimState;
        input.PlayerActions.ThrowRecallRight.canceled += HandleRightShootRecall;
    }

    /// <summary>
    /// On s'abonne � l'input qui sert � Shoot et Recall l'objet de gauche
    /// </summary>
    protected virtual void AddInputLeftThrowRecall()
    {
        input.PlayerActions.ThrowRecallLeft.performed += HandleAimState;
        input.PlayerActions.ThrowRecallLeft.canceled += HandleLeftShootRecall;
    }
    
    /// <summary>
    /// On se d�sabonne � l'input qui sert � shoot et recall l'objet de droite
    /// </summary>
    protected virtual void RemoveInputRightThrowRecall()
    {
        input.PlayerActions.ThrowRecallRight.performed -= HandleAimState;
        input.PlayerActions.ThrowRecallRight.canceled -= HandleRightShootRecall;
    }

    /// <summary>
    /// On se d�sabonne � l'input qui sert � shoot et recall l'objet de gauche
    /// </summary>
    protected virtual void RemoveInputLeftThrowRecall()
    {
        input.PlayerActions.ThrowRecallLeft.performed -= HandleAimState;
        input.PlayerActions.ThrowRecallRight.canceled -= HandleLeftShootRecall;
    }
    #endregion

    #region Attraction

    protected virtual void AddInputRightAttraction()
    {
        input.PlayerActions.AttractionRight.started += HandleRightAttraction;
    }

    protected virtual void AddInputLeftAttraction()
    {
        input.PlayerActions.AttractionLeft.started += HandleLeftAttraction;
    }
    
    protected virtual void RemoveInputRightAttraction()
    {
        input.PlayerActions.AttractionRight.started -= HandleRightAttraction;
    }

    protected virtual void RemoveInputLeftAttraction()
    {
        input.PlayerActions.AttractionLeft.started -= HandleLeftAttraction;
    }

    #endregion
    
    #endregion

    #region Ability methods

    /// <summary>
    /// M�thode appel� quand le joueur est abonn� � l'input Ability et appuie sur l'input
    /// </summary>
    /// <param name="context"></param>
    protected void HandleAimState(InputAction.CallbackContext context)
    {
        _stateMachine.ChangeState(_stateMachine.AimState);
    }
    
    private void HandleRightShootRecall(InputAction.CallbackContext context)
    {
        if (reusableData.RightProjectile == null)
        {
            _stateMachine.ChangeState(_stateMachine.ShootState);
        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.RecallState);
        }
    }

    protected void HandleRightShootRecall()
    {
        if (reusableData.RightProjectile == null)
        {
            _stateMachine.ChangeState(_stateMachine.ShootState);
        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.RecallState);
        }
    }
    
    private void HandleLeftShootRecall(InputAction.CallbackContext context)
    {
        if (reusableData.LeftProjectile == null)
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
        if (reusableData.LeftProjectile == null)
        {
            _stateMachine.ChangeState(_stateMachine.ShootState);
        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.RecallState);
        }
    }
    
    private void HandleLeftAttraction(InputAction.CallbackContext obj)
    {
        if (reusableData.LeftProjectile == null)
        {
            return;
        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.TransportState);
        }
    }
    
    protected void HandleLeftAttraction()
    {
        if (reusableData.LeftProjectile == null)
        {
            return;
        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.TransportState);
        }
    }

    private void HandleRightAttraction(InputAction.CallbackContext context)
    {
        if (reusableData.RightProjectile == null)
        {
            return;
        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.TransportState);
        }
    }
    
    protected void HandleRightAttraction()
    {
        if (reusableData.RightProjectile == null)
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
