using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTransportState : AbilityState
{
    private Vector3 refVelocity;
    private Vector3 startPosition;

    private InteractionSystem leftInteraction;
    private InteractionSystem rightInteraction;
    private InteractionSystem playerInteraction;

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

        // Assignation variable car souvent utilisé
        playerInteraction = _stateMachine.AbilityManager.GetComponent<InteractionSystem>();

        if (reusableData.LeftObject != null)
        {
            leftInteraction = reusableData.LeftObject.GetComponent<InteractionSystem>();
        }
        
        if (reusableData.RightObject != null)
        {
            rightInteraction = reusableData.RightObject.GetComponent<InteractionSystem>();
        }

        if (input.PlayerActions.AttractionLeft.IsPressed())
        {
            OnLeftActivation?.Invoke();
            reusableData.LeftActivation = true;
            if (reusableData.RightObject == null)
            {
                // Player Move
                OnDash?.Invoke();
            }
        }
        
        if (input.PlayerActions.AttractionRight.IsPressed())
        {
            OnRightActivation?.Invoke();
            reusableData.RightActivation = true;
            if (reusableData.LeftObject == null)
            {
                // Player Move
                OnDash?.Invoke();
            }
        }
        
        if (CheckPlayerTransportState())
        {
            startPosition = _stateMachine.AbilityManager.transform.position;
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

        if (reusableData.RightActivation)
        {
            if (_stateMachine.AbilityManager.useStamina && metricsManager.StaminaRight <= 0)
            {
                reusableData.RightInput = true;
                _stateMachine.ChangeState(_stateMachine.RecallState);
                return;
            }
        }

        if (_stateMachine.AbilityManager.useStamina && reusableData.LeftActivation)
        {
            if (metricsManager.StaminaLeft <= 0)
            {
                reusableData.LeftInput = true;
                _stateMachine.ChangeState(_stateMachine.RecallState);
                return;
            }
        }
        
        if (input.PlayerActions.AttractionLeft.IsPressed())
        {
            if (reusableData.RightObject == null)
            {
                if (targetSystem.leftTargetLaunch.GetCurrentType() != InteractorType.Projectile)
                {
                    MovePlayer();
                }
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
                if (targetSystem.rightTargetLaunch.GetCurrentType() != InteractorType.Projectile)
                {
                    MovePlayer();
                }
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
        playerInteraction.Interactable(false);
        if (reusableData.LeftObject != null)
        {
            leftInteraction.Interactable(false);
            if (targetSystem.leftTargetLaunch.GetCurrentType() == InteractorType.Projectile)
            {
                reusableData.LeftObject.SetLaunch(true);
            }
        }

        if (reusableData.RightObject != null)
        {
            rightInteraction.Interactable(false);
            if (targetSystem.rightTargetLaunch.GetCurrentType() == InteractorType.Projectile)
            {
                reusableData.RightObject.SetLaunch(true);
            }
        }

        reusableData.LeftActivation = false;
        reusableData.RightActivation = false;
        reusableData.OnTransportation = false;
    }
    
    /// <summary>
    /// If LeftObjet and RightObject isn't null Player can Move in this State
    /// </summary>
    private bool CheckPlayerTransportState()
    {
        if (reusableData.RightActivation)
        {
            if (reusableData.LeftObject == null)
            {
                reusableData.OnTransportation = true;
                return true;
            }
        }

        if (reusableData.LeftActivation)
        {
            if (reusableData.RightObject == null)
            {
                reusableData.OnTransportation = true;
                return true;
            }
        }
        
        reusableData.OnTransportation = false;
        return false;
    }
    
    
    private void MoveLeftObject()
    {
        if (reusableData.LeftParent != null)
        {
            if (reusableData.RightObject != null)
            {
                rightInteraction.Interactable(true);
                
                if (reusableData.RightParent != null)
                {
                    // Move LeftParent to RightParent
                    reusableData.LeftObject.Move(reusableData.LeftParent, reusableData.RightParent);
                }
                else
                {
                    // Move LeftParent to RightObject
                    reusableData.LeftObject.Move(reusableData.LeftParent, reusableData.RightObject.gameObject);
                }
            }
            else
            {
                // Move LeftParent to Player
                playerInteraction.Interactable(true);
                reusableData.LeftObject.SetLaunch(false);
                reusableData.LeftObject.Move(reusableData.LeftParent, _stateMachine.AbilityManager.RightCloseLauncherTransform.gameObject);
            }
        }
        else
        {
            if (reusableData.RightObject != null)
            {
                rightInteraction.Interactable(true);
                
                if (reusableData.RightParent != null)
                {
                    // Move LeftObject to RightParent
                    reusableData.LeftObject.Move(reusableData.LeftObject.gameObject, reusableData.RightParent);
                }
                else
                {
                    // Move LeftObject to RightObject
                    reusableData.LeftObject.Move(reusableData.LeftObject.gameObject, reusableData.RightObject.gameObject);
                }
            }
            else
            {
                // Move LeftObject to Player
                playerInteraction.Interactable(true);
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
                leftInteraction.Interactable(true);
                
                if (reusableData.LeftParent != null)
                {
                    // Move RightParent to LeftParent
                    reusableData.RightObject.Move(reusableData.RightParent, reusableData.LeftParent);
                }
                else
                {
                    // Move RightParent to LeftObject
                    reusableData.RightObject.Move(reusableData.RightParent, reusableData.LeftObject.gameObject);
                }
            }
            else
            {
                // Move RightParent to Player
                playerInteraction.Interactable(true);
                reusableData.RightObject.SetLaunch(false);
                reusableData.RightObject.Move(reusableData.RightParent, _stateMachine.AbilityManager.LeftCloseLauncherTransform.gameObject);
            }
        }
        else
        {
            if (reusableData.LeftObject != null)
            {
                leftInteraction.Interactable(true);
                
                if (reusableData.LeftParent != null)
                {
                    // Move RightObject to LeftParent
                    reusableData.RightObject.Move(reusableData.RightObject.gameObject, reusableData.LeftParent);
                }
                else
                {
                    // Move RightObject to LeftObject
                    reusableData.RightObject.Move(reusableData.RightObject.gameObject, reusableData.LeftObject.gameObject);
                }
            }
            else
            {
                // Move RightObject to Player
                playerInteraction.Interactable(true);
                reusableData.RightObject.Move(reusableData.RightObject.gameObject, _stateMachine.AbilityManager.LeftLauncherTransform.gameObject);
            }
        }
    }

    private void MovePlayer()
    {
        if (reusableData.RightObject != null)
        {
            rightInteraction.Interactable(true);
            playerInteraction.Interactable(true);
            
            // Move Player to RightObject
            Vector3 direction = (reusableData.RightObject.transform.position -
                                  _stateMachine.AbilityManager.LeftLauncherTransform.position).normalized;

            float currentSpeed = CalculateSpeed(reusableData.RightObject.transform.position);
            
            _stateMachine.AbilityManager.Rb.velocity = direction * (currentSpeed * metricsManager.ExternForce);
        }
        else
        {
            leftInteraction.Interactable(true);
            playerInteraction.Interactable(true);
            // Move Player to LeftObject
            Vector3 direction = (reusableData.LeftObject.transform.position -
                                 _stateMachine.AbilityManager.RightLauncherTransform.position).normalized;

            float currentSpeed = CalculateSpeed(reusableData.LeftObject.transform.position);
            
            _stateMachine.AbilityManager.Rb.velocity = direction * (currentSpeed * metricsManager.ExternForce);
        }
    }
    
    private float CalculateSpeed(Vector3 targetPosition)
    {
        float totalDistance = Vector3.Distance(startPosition, targetPosition);
        
        float currentDistance = Vector3.Distance(startPosition, _stateMachine.AbilityManager.transform.position);
        
        float currentPercentageDistance = currentDistance / totalDistance;

        float currentSpeedMultiplier =
            metricsManager.CurrentMetrics.AbilityData.TransportCurve.Evaluate(currentPercentageDistance);
        
        SpeedModifierEvent?.Invoke(currentPercentageDistance);
        
        if (currentSpeedMultiplier < 0.1f)
        {
            currentSpeedMultiplier = 0.1f;
        }
        
        return  currentSpeedMultiplier * metricsManager.CurrentMetrics.AbilityData.TransportPlayerSpeed;
    }
}
