using System;
using UnityEngine;

public class AbilityMovePlayer : AbilityTransportState
{
    private Vector3 refVelocity;
    private Vector3 startPosition;

    // Selon ce vers quoi tu te dirige l'origine peut être au pied du joueur ou au niveau de la tête
    private Vector3 relativeOrigin;
    
    public event Action<float> SpeedModifierEvent;

    public event Action EnterDash;
    public event Action ExitDash;
    
    public event Action<int> OnConsecutiveDashes;

    public event Action OnFlyingDance;
    
    public AbilityMovePlayer(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        reusableData.NumberOfConsecutiveTransportation++;
        OnConsecutiveDashes?.Invoke(reusableData.NumberOfConsecutiveTransportation);

        if (reusableData.NumberOfConsecutiveTransportation > 2)
        {
            OnFlyingDance?.Invoke();
        }

        playerInteraction = _stateMachine.AbilityManager.GetComponent<PlayerInteraction>();

        if (reusableData.RightObject)
        {
            if (reusableData.RightParent)
            {
                rightInteraction = reusableData.RightParent.GetComponent<InteractionSystem>();
            }
            else
            {
                rightInteraction = reusableData.RightObject.GetComponent<InteractionSystem>();
            }
            
            rightInteraction.Interactable(true);
            reusableData.RightActivation = true;
            if (rightInteraction.interactorType == InteractorType.Enemy)
            {
                relativeOrigin = _stateMachine.AbilityManager.AimTransform.position;
            }
            else
            {
                relativeOrigin = _stateMachine.AbilityManager.transform.position;
            }
            
            if (_stateMachine.AbilityManager.Metrics.useCharge)
            {
                _stateMachine.AbilityManager.Metrics.ConsumeCharge(true);
            }
        }
        else
        {
            if (reusableData.LeftParent)
            {
                leftInteraction = reusableData.LeftParent.GetComponent<InteractionSystem>();
            }
            else
            {
                leftInteraction = reusableData.LeftObject.GetComponent<InteractionSystem>();
            }
            
            leftInteraction.Interactable(true);
            reusableData.LeftActivation = true;
            if (leftInteraction.interactorType == InteractorType.Enemy)
            {
                relativeOrigin = _stateMachine.AbilityManager.AimTransform.position;
            }
            else
            {
                relativeOrigin = _stateMachine.AbilityManager.transform.position;
            }
            
            if (_stateMachine.AbilityManager.Metrics.useCharge)
            {
                _stateMachine.AbilityManager.Metrics.ConsumeCharge(false);
            }
        }
        
        EnterDash?.Invoke();
        reusableData.OnTransportation = true;
        startPosition = _stateMachine.AbilityManager.transform.position;
    }

    public override void Tick()
    {
        base.Tick();
        
        if (reusableData.RightObject == null && reusableData.LeftObject == null)
        {
            reusableData.RightParent = null;
            reusableData.LeftParent = null;
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }
        
        if (_stateMachine.AbilityManager.kinichMode)
        {
            if (reusableData.LeftActivation)
            {
                if (reusableData.LeftParent == null)
                {
                    if (!metricsManager.useCharge)
                    {
                        CheckStamina();
                    }
                }
            }

            if (reusableData.RightActivation)
            {
                if (reusableData.RightParent == null)
                {
                    if (!metricsManager.useCharge)
                    {
                        CheckStamina();
                    }
                }
            }
        }
    }

    public override void FixedTick()
    {
        base.FixedTick();
        
        MovePlayer();
    }

    public override void Exit()
    {
        base.Exit();
        playerInteraction.Interactable(false);
        ExitDash?.Invoke();
        reusableData.OnTransportation = false;
        leftInteraction = null;
        rightInteraction = null;
        reusableData.LeftActivation = false;
        reusableData.RightActivation = false;
    }

    private void MovePlayer()
    {
        var rb = _stateMachine.AbilityManager.Rb;
        
        if (reusableData.RightObject != null)
        {
            playerInteraction.Interactable(true);
            
            // Move Player to RightObject
            if (reusableData.RightObject != null)
            {
                Vector3 direction = (reusableData.RightObject.transform.position -
                                     relativeOrigin).normalized;
            

                float currentSpeed = CalculateSpeed(reusableData.RightObject.transform.position);
            
                rb.velocity = direction * (currentSpeed * metricsManager.ExternForce);
            }
        }
        else
        {
            playerInteraction.Interactable(true);
            
            // Move Player to LeftObject
            if (reusableData.LeftObject != null)
            {
                Vector3 direction = (reusableData.LeftObject.transform.position -
                                     relativeOrigin).normalized;

                float currentSpeed = CalculateSpeed(reusableData.LeftObject.transform.position);
            
                rb.velocity = direction * (currentSpeed * metricsManager.ExternForce);
            }
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
