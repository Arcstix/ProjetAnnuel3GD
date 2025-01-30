using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTransportState : AbilityState
{
    private Vector3 refVelocity;
    private Vector3 startPosition;
    private float timer;

    public event Action OnRightActivation;
    public event Action OnLeftActivation;
    public event Action OnDash;
    
    public event Action<float> SpeedModifierEvent;
    
    public AbilityTransportState(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        timer = 0;
        if (CheckPlayerTransportState())
        {
            startPosition = _stateMachine.AbilityManager.transform.position;
        }

        if (input.PlayerActions.AttractionLeft.IsPressed())
        {
            OnLeftActivation?.Invoke();
            if (reusableData.RightObject == null)
            {
                // Player Move
                OnDash?.Invoke();
            }
        }
        
        if (input.PlayerActions.AttractionRight.IsPressed())
        {
            OnRightActivation?.Invoke();
            if (reusableData.LeftObject == null)
            {
                // Player Move
                OnDash?.Invoke();
            }
        }
    }

    public override void Tick()
    {
        base.Tick();
        
        // Important car on ne peut pas rester dans cette state si ce n'est pas vérifié.
        if (reusableData.RightObject == null && reusableData.LeftObject == null)
        {
            reusableData.LeftParent = null;
            reusableData.RightParent = null;
            
            leftLauncher.GetComponent<MeshRenderer>().enabled = true;
            rightLauncher.GetComponent<MeshRenderer>().enabled = true;
            
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }
        
        if (input.PlayerActions.AttractionLeft.IsPressed())
        {
            if (reusableData.RightObject == null)
            {
                MovePlayer();
            }
            else
            {
                MoveRightObject();
            }
        }

        if (input.PlayerActions.AttractionRight.IsPressed())
        {
            if (reusableData.LeftObject == null)
            {
                MovePlayer();
            }
            else
            {
                MoveLeftObject();
            }
        }
        
        if (input.PlayerActions.AttractionLeft.WasReleasedThisFrame() && !input.PlayerActions.AttractionRight.IsPressed())
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }

        if (input.PlayerActions.AttractionRight.WasReleasedThisFrame() && !input.PlayerActions.AttractionLeft.IsPressed())
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _stateMachine.AbilityManager.GetComponent<InteractionManager>().Activate(false);
        if (reusableData.LeftObject != null)
        {
            if (reusableData.LeftParent != null)
            {
                reusableData.LeftParent.GetComponent<InteractionManager>().Activate(false);
            }
            reusableData.LeftObject.GetComponent<InteractionManager>().Activate(false);
        }

        if (reusableData.RightObject != null)
        {
            if (reusableData.RightParent != null)
            {
                reusableData.RightParent.GetComponent<InteractionManager>().Activate(false);
            }
            reusableData.RightObject.GetComponent<InteractionManager>().Activate(false);
        }
        reusableData.OnTransportation = false;
    }
    
    /// <summary>
    /// If LeftObjet and RightObject isn't null Player can Move in this State
    /// </summary>
    private bool CheckPlayerTransportState()
    {
        if (reusableData.RightObject != null && reusableData.LeftObject != null)
        {
            reusableData.OnTransportation = false;
            return false;
        }
        else
        {
            reusableData.OnTransportation = true;
            return true;
        }
    }
    
    
    private void MoveLeftObject()
    {
        if (reusableData.LeftParent != null)
        {
            if (reusableData.RightObject != null)
            {
                if (reusableData.RightParent != null)
                {
                    // Move LeftParent to RightParent
                    reusableData.RightParent.GetComponent<InteractionManager>().Activate(true);
                    reusableData.LeftObject.Move(reusableData.LeftParent, reusableData.RightParent);
                }
                else
                {
                    // Move LeftParent to RightObject
                    reusableData.RightObject.GetComponent<InteractionManager>().Activate(true);
                    reusableData.LeftObject.Move(reusableData.LeftParent, reusableData.RightObject.gameObject);
                }
            }
            else
            {
                // Move LeftParent to Player
                _stateMachine.AbilityManager.GetComponent<InteractionManager>().Activate(true);
                reusableData.LeftObject.Move(reusableData.LeftParent, _stateMachine.AbilityManager.RightLauncherTransform.gameObject);
            }
        }
        else
        {
            if (reusableData.RightObject != null)
            {
                if (reusableData.RightParent != null)
                {
                    // Move LeftObject to RightParent
                    reusableData.RightParent.GetComponent<InteractionManager>().Activate(true);
                    reusableData.LeftObject.Move(reusableData.LeftObject.gameObject, reusableData.RightParent);
                }
                else
                {
                    // Move LeftObject to RightObject
                    reusableData.RightObject.GetComponent<InteractionManager>().Activate(true);
                    reusableData.LeftObject.Move(reusableData.LeftObject.gameObject, reusableData.RightObject.gameObject);
                }
            }
            else
            {
                // Move LeftObject to Player
                _stateMachine.AbilityManager.GetComponent<InteractionManager>().Activate(true);
                reusableData.LeftObject.Move(reusableData.LeftObject.gameObject, _stateMachine.AbilityManager.RightLauncherTransform.gameObject);
            }
        }
    }

    private void MoveRightObject()
    {
        if (reusableData.RightParent != null)
        {
            if (reusableData.LeftObject != null)
            {
                if (reusableData.LeftParent != null)
                {
                    // Move RightParent to LeftParent
                    reusableData.LeftParent.GetComponent<InteractionManager>().Activate(true);
                    reusableData.RightObject.Move(reusableData.RightParent, reusableData.LeftParent);
                }
                else
                {
                    // Move RightParent to LeftObject
                    reusableData.LeftObject.GetComponent<InteractionManager>().Activate(true);
                    reusableData.RightObject.Move(reusableData.RightParent, reusableData.LeftObject.gameObject);
                }
            }
            else
            {
                // Move RightParent to Player
                _stateMachine.AbilityManager.GetComponent<InteractionManager>().Activate(true);
                reusableData.RightObject.Move(reusableData.RightParent, _stateMachine.AbilityManager.LeftLauncherTransform.gameObject);
            }
        }
        else
        {
            if (reusableData.LeftObject != null)
            {
                if (reusableData.LeftParent != null)
                {
                    // Move RightObject to LeftParent
                    reusableData.LeftParent.GetComponent<InteractionManager>().Activate(true);
                    reusableData.RightObject.Move(reusableData.RightObject.gameObject, reusableData.LeftParent);
                }
                else
                {
                    // Move RightObject to LeftObject
                    reusableData.LeftObject.GetComponent<InteractionManager>().Activate(true);
                    reusableData.RightObject.Move(reusableData.RightObject.gameObject, reusableData.LeftObject.gameObject);
                }
            }
            else
            {
                // Move RightObject to Player
                _stateMachine.AbilityManager.GetComponent<InteractionManager>().Activate(true);
                reusableData.RightObject.Move(reusableData.RightObject.gameObject, _stateMachine.AbilityManager.LeftLauncherTransform.gameObject);
            }
        }
    }

    private void MovePlayer()
    {
        if (reusableData.RightObject != null)
        {
            if (reusableData.RightParent != null)
            {
                reusableData.RightParent.GetComponent<InteractionManager>().Activate(true);
            }
            else
            {
                reusableData.RightObject.GetComponent<InteractionManager>().Activate(true);
            }
            
            // Move Player to RightObject
            Vector3 direction = (reusableData.RightObject.transform.position -
                                  _stateMachine.AbilityManager.LeftLauncherTransform.position).normalized;

            float currentSpeed = CalculateSpeed(reusableData.RightObject.transform.position);
            
            _stateMachine.AbilityManager.Rb.velocity = direction * (currentSpeed * metricsManager.ExternForce);
        }
        else
        {
            if (reusableData.LeftParent != null)
            {
                reusableData.LeftParent.GetComponent<InteractionManager>().Activate(true);
            }
            else
            {
                reusableData.LeftObject.GetComponent<InteractionManager>().Activate(true);
            }
            
            // Move Player to LeftObject
            Vector3 direction = (reusableData.LeftObject.transform.position -
                                 _stateMachine.AbilityManager.RightLauncherTransform.position).normalized;

            float currentSpeed = CalculateSpeed(reusableData.LeftObject.transform.position);
            
            _stateMachine.AbilityManager.Rb.velocity = direction * currentSpeed;
        }
    }
    
    private float CalculateSpeed(Vector3 targetPosition)
    {
        float totalDistance = Vector3.Distance(startPosition, targetPosition);
        
        float currentDistance = Vector3.Distance(startPosition, _stateMachine.AbilityManager.transform.position);
        
        float currentPercentageDistance = currentDistance / totalDistance;

        float currentSpeedMultiplier =
            metricsManager.CurrentPlayerSO.AbilityData.TransportCurve.Evaluate(currentPercentageDistance);
        
        SpeedModifierEvent?.Invoke(currentPercentageDistance);
        
        if (currentSpeedMultiplier < 0.1f)
        {
            currentSpeedMultiplier = 0.1f;
        }
        
        return  currentSpeedMultiplier * metricsManager.CurrentPlayerSO.AbilityData.TransportPlayerSpeed;
    }
}
